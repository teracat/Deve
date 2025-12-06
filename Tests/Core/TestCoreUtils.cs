using Deve.Core;
using Deve.Data;
using Deve.Model;
using Moq;

namespace Deve.Tests.Core
{
    public class TestCoreUtils
    {
        #region CheckIdWhenAdding
        [Fact]
        public void CheckIdWhenAdding_NonExistingId_ReturnsNull()
        {
            var options = new Mock<IDataOptions>().Object;
            var data = new ModelId { Id = 1 };
            var list = new List<ModelId> { new() { Id = 2 } };

            var result = UtilsCore.CheckIdWhenAdding(options, data, list);

            Assert.Null(result);
        }

        [Fact]
        public void CheckIdWhenAdding_ExistingId_ReturnsErrorDuplicatedValue()
        {
            var options = new Mock<IDataOptions>().Object;
            var data = new ModelId { Id = 1 };
            var list = new List<ModelId> { new() { Id = 1 } };

            var result = UtilsCore.CheckIdWhenAdding(options, data, list);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(ResultErrorType.DuplicatedValue, result.Errors[0].Type);
        }

        [Fact]
        public void CheckIdWhenAdding_NullData_ThrowsException()
        {
            var options = new Mock<IDataOptions>().Object;
            ModelId? data = null;
            var list = new List<ModelId?> { new() { Id = 1 } };

#pragma warning disable CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
            _ = Assert.Throws<ArgumentNullException>(() => UtilsCore.CheckIdWhenAdding(options, data, list));
#pragma warning restore CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
        }
        #endregion

        #region GetCacheKeyForType
        [Fact]
        public void GetCacheKeyForType_TestCoreUtils5_ReturnsTestCoreUtils5()
        {
            var result = UtilsCore.GetCacheKeyForType<TestCoreUtils>(5);

            Assert.Equal("TestCoreUtils-5", result);
        }

        [Fact]
        public void GetCacheKeyForType_TestCoreStats1_ReturnsTestCoreStats1()
        {
            var result = UtilsCore.GetCacheKeyForType<TestCoreStats>(1);

            Assert.Equal("TestCoreStats-1", result);
        }
        #endregion
    }
}