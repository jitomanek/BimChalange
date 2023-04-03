using Custom.Lib.Models.Repository;
using Custom.Lib.Util;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Net.Http.Headers;
using System.Text;

namespace Custom.Lib.Repository
{
    public class RepositoryBase
    {
        protected readonly DbContext _baseContext;
        protected readonly ILogger _logger;


        public RepositoryBase(DbContext baseContext,
            ILoggerFactory loggerFactory)
        {
            _baseContext = baseContext;
            _logger = loggerFactory.CreateLogger<RepositoryBase>();
        }

        public async Task<T> GetItem<T>(int id) where T : class
        {
            return await _baseContext.FindAsync<T>(new object[] { id });
        }

        public async Task<T> GetItem<T>(object[] id) where T : class
        {
            return await _baseContext.FindAsync<T>(id);
        }

        public async Task<T> CreateItem<T>(T model) where T : class
        {
            await _baseContext.AddAsync<T>(model);
            await _baseContext.SaveChangesAsync();

            return model;
        }

        public async Task SaveChanges()
        {
            await _baseContext.SaveChangesAsync();
        }

        #region TableQueries

        public async Task<DataTableReply<T>> GetTable<T>(DataTable model, CancellationToken token, IQueryable<T>? query = null) where T : class
        {
            var retQuery = query ?? GetDbSet<T>();

            retQuery = GetFilter<T>(model.FilterArray, retQuery).AsNoTracking();

            if (!_baseContext.Database.ProviderName.EndsWith(".InMemory"))
                using (var transact = _baseContext.Database.BeginTransaction())
                {
                    var dataModel = new DataTableReply<T>
                    {
                        Count = await retQuery.CountAsync(),
                        Data = await GetList<T>(token, retQuery.Skip(model.From).Take(model.Count)),
                    };

                    return dataModel;
                }
            else
                return await GetTableInMemoryDB(retQuery, model, token);
        }

        /// <summary>
        /// Db InMemory - cannot use transactions
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        protected async Task<DataTableReply<T>> GetTableInMemoryDB<T>(IQueryable<T> query, DataTable model, CancellationToken token) where T : class
        {
            return new DataTableReply<T>
            {
                Count = query.Count(),
                Data = await GetList<T>(token, query.Skip(model.From).Take(model.Count))
            };
        }

        protected IQueryable<T> GetFilter<T>(DataTableFilter[] filter, IQueryable<T> query) where T : class
        {
            var builder = new StringBuilder();
            var type = typeof(T);
            string or = string.Empty;

            filter = filter.Where(x => !String.IsNullOrWhiteSpace(x.PropValue)).ToArray();
            for (int i = 0; i < filter.Length; i++)
            {
                var name = filter[i].PropName.FirstToUpper();

                if (i == 1)
                    or = " || ";

                switch (filter[i].FilterType)
                {
                    case FilterType.Equals:
                        builder.Append($"{or}{name} == \"{filter[i].PropValue.Trim()}\"");
                        break;
                    case FilterType.NotEquals:
                        builder.Append($"{or}{name} != \"{filter[i].PropValue.Trim()}\"");
                        break;
                    case FilterType.EndsWith:
                        builder.Append($"{or}{name}.EndsWith(\"{filter[i].PropValue.Trim()}\")");
                        break;
                    case FilterType.Contains:
                        builder.Append($"{or}{name}.Contains(\"{filter[i].PropValue.Trim()}\")");
                        break;
                    case FilterType.StartsWith:
                        builder.Append($"{or}{name}.StartsWith(\"{filter[i].PropValue.Trim()}\")");
                        break;
                    case FilterType.Flags:
                        builder.Append(or);
                        builder.Append(String.Join(" && ", filter[i].PropValue.Split(',').Select(x => $"({name} & {x.Trim()})=={x.Trim()}")));
                        break;
                    case FilterType.MultiEqual:
                        builder.Append(or);
                        builder.Append(String.Join(" || ", filter[i].PropValue.Split(',').Select(x => $"{name} =={x.Trim()}")));
                        break;
                    default:
                        builder.Append($"{or}{name}.Contains(\"{filter[i].PropValue.Trim()}\")");
                        break;
                }
            }

            or = builder.ToString();

            return String.IsNullOrWhiteSpace(or) ? query : query.Where(or);
        }

        public async Task<T[]> GetList<T>(CancellationToken token, IQueryable<T>? query = null) where T : class
        {
            var retQuery = query ?? GetDbSet<T>();

            if (retQuery == null)
            {
                throw new ArgumentException($"DbSet of type:{nameof(T)} not found in dbContext:{nameof(_baseContext)}");
            }

            return await retQuery.ToArrayAsync(token);
        }

        public DbSet<T> GetDbSet<T>() where T : class
        {
            return (DbSet<T>)_baseContext.GetType()
                 .GetProperty(typeof(T)?.Name ?? "", typeof(DbSet<T>))
                 .GetValue(_baseContext);
        }

        #endregion
    }
}
