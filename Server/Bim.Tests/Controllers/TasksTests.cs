namespace Bim.Tests.Controllers
{
    [TestClass]
    public partial class TaskTests
    {

        protected static object[] createTableData
        {
            get
            {
                return new[]
                {
                   new object[]
                   {
                       new TaskCreateRequest { Name="Test task", Description="one", Priority=1,Status=TaskStatusEnum.Initial }
                   },
                    new object[]
                   {
                       new TaskCreateRequest { Name="Test second", Description="second", Priority=2,Status=TaskStatusEnum.Initial }
                   }
                };
            }
        }


        protected static object[] tableData
        {
            get
            {
                return new[]
                {
                   new object[]
                   {
                       null,
                       0
                   },
                    new object[]
                   {
                       new TaskTableRequest(),
                       2
                   },
                    new object[]
                   {
                       new TaskTableRequest{ Name=new DataTableFilter{PropValue="test" } },
                       0
                   },
                    new object[]
                   {
                       new TaskTableRequest{ Name=new DataTableFilter{PropValue="test", FilterType=FilterType.Contains } },
                       2
                   },
                     new object[]
                   {
                       new TaskTableRequest{ Name=new DataTableFilter{PropValue="second", FilterType=FilterType.Contains } },
                       1
                   }
                };
            }
        }


        [TestMethod]
        [DynamicData(nameof(createTableData))]
        public async Task ZCreateData(TaskCreateRequest model)
        {
            await Initializer._client.TaskPOSTAsync(model);
        }

        [TestMethod]
        [DynamicData(nameof(tableData))]
        public async Task ZTableTest(TaskTableRequest model, int? expectedCount)
        {
            try
            {
                var actual = await Initializer._client.TasksAsync(model);

                Assert.AreEqual(expectedCount, actual.Count);
            }
            catch (ArgumentOutOfRangeException ex) { }
        }


    }
}
