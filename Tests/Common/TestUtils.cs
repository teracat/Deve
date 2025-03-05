using Deve.Model;

namespace Deve.Tests
{
    public class TestUtils
    {
        #region SomeIsNullOrWhiteSpace
        [Fact]
        public void SomeIsNullOrWhiteSpace_Null_ReturnTrue()
        {
            Assert.True(Utils.SomeIsNullOrWhiteSpace("a", null));
        }

        [Fact]
        public void SomeIsNullOrWhiteSpace_Empty_ReturnTrue()
        {
            Assert.True(Utils.SomeIsNullOrWhiteSpace("a", ""));
        }

        [Fact]
        public void SomeIsNullOrWhiteSpace_Space_ReturnTrue()
        {
            Assert.True(Utils.SomeIsNullOrWhiteSpace("a", " "));
        }

        [Fact]
        public void SomeIsNullOrWhiteSpace_NotEmpty_ReturnFalse()
        {
            Assert.False(Utils.SomeIsNullOrWhiteSpace("a", "b"));
        }
        #endregion

        #region FindNullOrWhiteSpace
        [Fact]
        public void FindNullOrWhiteSpace_NullArray_ReturnNotNullAndEmpty()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var res = Utils.FindNullOrWhiteSpace(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.NotNull(res);
            Assert.Empty(res);
        }

        [Fact]
        public void FindNullOrWhiteSpace_NullField_ReturnNotNullAndEmpty()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Field[] fields = [null];
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            var res = Utils.FindNullOrWhiteSpace(fields);

            Assert.NotNull(res);
            Assert.Empty(res);
        }

        [Fact]
        public void FindNullOrWhiteSpace_FieldNoValue_ReturnNotEmpty()
        {
            Field[] fields = [new Field()];

            var res = Utils.FindNullOrWhiteSpace(fields);

            Assert.NotEmpty(res);
        }

        [Fact]
        public void FindNullOrWhiteSpace_FieldEmptyString_ReturnNotEmpty()
        {
            Field[] fields = [new Field("")];

            var res = Utils.FindNullOrWhiteSpace(fields);

            Assert.NotEmpty(res);
        }

        [Fact]
        public void FindNullOrWhiteSpace_FieldSpace_ReturnNotEmpty()
        {
            Field[] fields = [new Field(" ")];

            var res = Utils.FindNullOrWhiteSpace(fields);

            Assert.NotEmpty(res);
        }

        [Fact]
        public void FindNullOrWhiteSpace_FieldStringValue_ReturnEmpty()
        {
            Field[] fields = [new Field("a")];

            var res = Utils.FindNullOrWhiteSpace(fields);

            Assert.Empty(res);
        }

        [Fact]
        public void FindNullOrWhiteSpace_FieldZero_ReturnNotEmpty()
        {
            Field[] fields = [new Field(0)];

            var res = Utils.FindNullOrWhiteSpace(fields);

            Assert.NotEmpty(res);
        }

        [Fact]
        public void FindNullOrWhiteSpace_Negative_ReturnNotEmpty()
        {
            Field[] fields = [new Field(-1)];

            var res = Utils.FindNullOrWhiteSpace(fields);

            Assert.NotEmpty(res);
        }

        [Fact]
        public void FindNullOrWhiteSpace_FieldOne_ReturnEmpty()
        {
            Field[] fields = [new Field(1)];

            var res = Utils.FindNullOrWhiteSpace(fields);

            Assert.Empty(res);
        }
        #endregion

        #region IsEmptyValue
        [Fact]
        public void IsEmptyValue_Null_ReturnTrue()
        {
            Assert.True(Utils.IsEmptyValue(null));
        }

        [Fact]
        public void IsEmptyValue_Empty_ReturnTrue()
        {
            Assert.True(Utils.IsEmptyValue(""));
        }

        [Fact]
        public void IsEmptyValue_Space_ReturnTrue()
        {
            Assert.True(Utils.IsEmptyValue(" "));
        }

        [Fact]
        public void IsEmptyValue_Zero_ReturnTrue()
        {
            Assert.True(Utils.IsEmptyValue(0));
        }

        [Fact]
        public void IsEmptyValue_Negative_ReturnTrue()
        {
            Assert.True(Utils.IsEmptyValue(-1));
        }

        [Fact]
        public void IsEmptyValue_NotEmpty_ReturnFalse()
        {
            Assert.False(Utils.IsEmptyValue("test"));
        }

        [Fact]
        public void IsEmptyValue_One_ReturnFalse()
        {
            Assert.False(Utils.IsEmptyValue(1));
        }
        #endregion

        #region FoundFieldsToErrors
        [Fact]
        public void FoundFieldsToErrors_NullLangAndNullList_ReturnNotNullAndEmpty()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var res = Utils.FoundFieldsToErrors(null, ResultErrorType.Unknown, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.NotNull(res);
            Assert.Empty(res);
        }

        [Fact]
        public void FoundFieldsToErrors_NotNullLangAndNullList_ReturnNotNullAndEmpty()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var res = Utils.FoundFieldsToErrors(Constants.DefaultLangCode, ResultErrorType.Unknown, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.NotNull(res);
            Assert.Empty(res);
        }

        [Fact]
        public void FoundFieldsToErrors_NullLangAndEmptyList_ReturnNotNullAndEmpty()
        {
            List<Field> list = [];

            var res = Utils.FoundFieldsToErrors(Constants.DefaultLangCode, ResultErrorType.Unknown, list);

            Assert.NotNull(res);
            Assert.Empty(res);
        }

        [Fact]
        public void FoundFieldsToErrors_OneField_ReturnListWithOneItem()
        {
            List<Field> list = [new Field()];

            var res = Utils.FoundFieldsToErrors(Constants.DefaultLangCode, ResultErrorType.Unknown, list);

            Assert.Single(res);
        }

        [Fact]
        public void FoundFieldsToErrors_TwoFields_ReturnListWithTwoItems()
        {
            List<Field> list = [new Field(), new Field()];

            var res = Utils.FoundFieldsToErrors(Constants.DefaultLangCode, ResultErrorType.Unknown, list);

            Assert.Equal(2, res.Count);
        }
        #endregion

        #region CreateInstance
        [Fact]
        public void CreateInstance_Result_ReturnResultType()
        {
            var obj = Utils.CreateInstance<Result>();

            Assert.IsType<Result>(obj);
        }
        #endregion

        #region ErrorsToString
        [Fact]
        public void ErrorsToString_Null_ReturnEmptyString()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var res = Utils.ErrorsToString(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.Empty(res);
        }

        [Fact]
        public void ErrorsToString_Empty_ReturnEmptyString()
        {
            List<ResultError> errors = [];

            var res = Utils.ErrorsToString(errors);

            Assert.Empty(res);
        }

        [Fact]
        public void ErrorsToString_OneEmptyError_ReturnEmptyString()
        {
            List<ResultError> errors = [new ResultError(ResultErrorType.Unknown)];

            var res = Utils.ErrorsToString(errors);

            Assert.Empty(res);
        }

        [Fact]
        public void ErrorsToString_NotEmptyError_ReturnNotEmptyString()
        {
            List<ResultError> errors = [new ResultError(ResultErrorType.Unknown, "field", "description")];

            var res = Utils.ErrorsToString(errors);

            Assert.NotEmpty(res);
        }

        [Fact]
        public void ErrorsToString_TwoErrors_ReturnNotEmptyString()
        {
            List<ResultError> errors = [
                new ResultError(ResultErrorType.Unknown, "field1", "description1"),
                new ResultError(ResultErrorType.Unknown, "field2", "description2")
            ];

            var res = Utils.ErrorsToString(errors);

            Assert.NotEmpty(res);
        }
        #endregion
    }
}