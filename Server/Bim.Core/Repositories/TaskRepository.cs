using Bim.Core.Entity;

using Custom.Lib.Repository;

using Microsoft.Extensions.Logging;

namespace Bim.Core.Repositories
{
    public class TaskRepository : RepositoryBase
    {
        public TaskRepository(BimContext context, ILoggerFactory loggerFactory)
            : base(context, loggerFactory) { }


    }
}
