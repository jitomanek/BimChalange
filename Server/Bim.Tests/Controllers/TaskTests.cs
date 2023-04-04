using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bim.Tests.Controllers
{
    public partial class TaskTests
    {

        #region DynamicTestData
        protected static object[] createData
        {
            get
            {
                return new[]
                {
                   new object[]{ null, null},

                   new object[]{
                       new TaskCreateRequest(),
                       new TaskResponse()
                   },
                   new object[]
                   {
                       new TaskCreateRequest { Name="Test task", Description="test", Priority=1,Status=TaskStatusEnum.Initial },
                       new TaskResponse{  Name="Test task", Description="test", Priority=1,Status=TaskStatusEnum.Initial, Id=1}
                   }
                };
            }
        }


        protected static object[] updateData
        {
            get
            {
                return new[]
                {
                   new object[]{ null, null},

                   new object[]{
                       new TaskUpdateRequest(),
                       new TaskResponse()
                   },
                   new object[]
                   {
                       new TaskUpdateRequest { Name="Test task - updated", Description="test updated", Priority=3,Status=TaskStatusEnum.Complete, Id=1 },
                       new TaskResponse{ Name="Test task - updated", Description="test updated", Priority=3,Status=TaskStatusEnum.Complete, Id=1}
                   },
                   new object[]
                   {
                       new TaskUpdateRequest { Name="Test task - ForbidUpdate", Description="test updated", Priority=3,Status=TaskStatusEnum.Complete, Id=1 },
                       new TaskResponse{ Name="Test task - updated", Description="test updated", Priority=3,Status=TaskStatusEnum.Complete, Id=1}
                   }
                };
            }
        }

        #endregion

        [TestMethod]
        [DynamicData(nameof(createData))]
        public async Task CreateTest(TaskCreateRequest model, TaskResponse expected)
        {
            try
            {
                var actual = await Initializer._client.TaskPOSTAsync(model);

                if (expected?.Name != null)
                {
                    Assert.AreEqual(expected.Name, actual.Name);
                    Assert.AreNotEqual(0, actual.Id);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {//first scenario
            }
            catch (JsonSerializationException ex)
            {//second scenario
            }

        }

        [TestMethod]
        [DynamicData(nameof(createData))]
        public async Task GetTest(TaskCreateRequest model, TaskResponse expected)
        {
            try
            {
                var actual = await Initializer._client.TaskGETAsync(expected?.Id);

                Assert.IsNotNull(actual);
                Assert.AreEqual(expected.Id, actual.Id);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                var a = ex;
            }
        }

        [TestMethod]
        [DynamicData(nameof(updateData))]
        public async Task UpdateTest(TaskUpdateRequest model, TaskResponse expected)
        {
            try
            {
                var actual = await Initializer._client.TaskPUTAsync(model);

                Assert.IsNotNull(actual);
                Assert.AreEqual(expected.Id, actual.Id);
                Assert.AreEqual(expected.Name, actual.Name);
            }
            catch (ArgumentOutOfRangeException ex)
            {//first scenario
            }
            catch (JsonSerializationException ex)
            {//second scenario
            }
        }

        [TestMethod]
        [DataRow(null, 1)]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        public async Task DeleteTest(int? taskId, int taskCount)
        {
            try
            {
                await Initializer._client.TaskDELETEAsync(taskId);
            }
            catch (ArgumentOutOfRangeException ex)
            {
            }

            var list = await Initializer._client.TasksAsync(new TaskTableRequest());
            Assert.AreEqual(taskCount, list.Count);
        }
    }
}
