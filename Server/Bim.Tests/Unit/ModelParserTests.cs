using Bim.Core.Entity.Models;
using Bim.Core.Models.View;
using Bim.Core.Parser;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bim.Tests.Unit
{
    [TestClass]
    public class ModelParserTests
    {
        #region DynamicTestData
        protected static object[] createEntityToResponse
        {
            get
            {
                return new[]
                {
                    new object[]
                    {
                        null,
                        null
                    },
                    new object[]
                   {
                       new TaskEntity(),
                       new TaskResponse()
                   },
                   new object[]
                   {
                       new TaskEntity { Name="Test one", Description="one", Priority=1,Status=TaskStatusEnum.Initial,Id=1 },
                       new TaskResponse { Name="Test one", Description="one", Priority=1,Status=TaskStatusEnum.Initial,Id=1 }
                   },

                };
            }
        }

        protected static object[] createRequestToEntity
        {
            get
            {
                return new[]
                {
                    new object[]
                    {
                        null,
                        null
                    },
                    new object[]
                   {
                       new TaskCreateRequest(),
                       new TaskEntity()
                   },
                   new object[]
                   {
                       new TaskCreateRequest { Name="Test one", Description="one", Priority=1,Status=TaskStatusEnum.Initial},
                       new TaskEntity { Name="Test one", Description="one", Priority=1,Status=TaskStatusEnum.Initial }
                   },

                };
            }
        }

        protected static object[] createUpdate
        {
            get
            {
                return new[]
                {
                    new object[]
                    {
                        null,
                        null,
                        null
                    },
                    new object[]
                   {
                       new TaskEntity(),
                       new TaskUpdateRequest(),
                       new TaskEntity()
                   },
                   new object[]
                   {
                       new TaskEntity { Name="Test one", Description="one", Priority=1,Status=TaskStatusEnum.Initial, Id=3 },
                       new TaskUpdateRequest { Name="Test update", Description="update", Priority=3,Status=TaskStatusEnum.InProgress},
                       new TaskEntity { Name="Test update", Description="update", Priority=3,Status=TaskStatusEnum.InProgress, Id=3 }
                   },

                };
            }
        }

        #endregion

        [TestMethod]
        [DynamicData(nameof(createEntityToResponse))]
        public void TaskEntityToTaskResponseTest(TaskEntity model, TaskResponse expected)
        {
            var actual = model.Parse();

            if (model != null)
                AssertParseTest<TaskResponse>(expected, actual);

            Assert.AreEqual(expected?.Name, actual?.Name);
        }

        [TestMethod]
        [DynamicData(nameof(createRequestToEntity))]
        public void TaskCreateRequestToTaskEntityTest(TaskCreateRequest model, TaskEntity expected)
        {
            var actual = model.Parse();

            if (model != null)
                AssertParseTest<TaskCreateRequest>(expected, actual);

            Assert.AreEqual(expected?.Name, actual?.Name);
        }

        [TestMethod]
        [DynamicData(nameof(createUpdate))]
        public void TaskEntityUpdateTest(TaskEntity model, TaskUpdateRequest updateRequest, TaskEntity expected)
        {
            model.Update(updateRequest);

            if (model != null)
                AssertParseTest<TaskEntity>(expected, model);

            Assert.AreEqual(expected?.Name, model?.Name);
        }

        /// <summary>
        /// Asserts inner propperties
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        protected void AssertParseTest<T>(object actual, object expected)
        {
            var keys = typeof(T).GetProperties().Where(x => x.MemberType == System.Reflection.MemberTypes.Property);
            foreach (var key in keys)
                Assert.AreEqual(
                    actual?.GetType().GetProperty(key.Name).GetValue(actual),
                    expected?.GetType().GetProperty(key.Name).GetValue(expected));
        }
    }
}
