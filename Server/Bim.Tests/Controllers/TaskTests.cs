using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bim.Tests.Controllers
{
    [TestClass]
    public class TaskTests
    {
        protected static object[] testData
        {
            get
            {
                return new[]
                {
                   new object[]{ null, null},

                   new object[]{
                       new TaskCreateRequest{ },
                       new TaskResponse{ }
                   },
                   new object[]
                   {
                       new TaskCreateRequest { Name="Test task", Priority=1,Status=TaskStatusEnum._0 },
                       new TaskResponse{  Name="Test task", Priority=1,Status=TaskStatusEnum._0}
                   }
                };
            }
        }


        [TestMethod]
        [DynamicData(nameof(testData))]
        public async Task CreateTest(TaskCreateRequest model, TaskResponse expected)
        {
            try
            {
                var actual = await Initializer._client.TaskPUTAsync(model);

                Assert.AreEqual(expected.Name, actual.Name);
                Assert.AreNotEqual(0, actual.Id);
            }catch(Exception ex)
            {
                var a= ex.InnerException;   
            }

        }

        //[TestMethod]
        //public async Task GetTest()
        //{
        //    var actual = await Initializer._client.TaskGETAsync(,);

        //}
    }
}
