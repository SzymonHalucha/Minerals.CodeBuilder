namespace Minerals.CodeBuilder.Tests
{
    [TestClass]
    public class CodeBuilderTests : VerifyBase
    {
        [ModuleInitializer]
        public static void InitializeVeify()
        {
            UseProjectRelativeDirectory("Snapshots");
            VerifierSettings.UseEncoding(Encoding.UTF8);
        }

        [TestMethod]
        public Task Write_ShouldGenerateOneLine()
        {
            var builder = new CodeBuilder();
            builder.Write("public").Write(" class ").Write("ExampleClass").OpenBlock().CloseBlock();
            return Verify(builder.ToString(), extension: "cs");
        }

        [TestMethod]
        public Task WriteLine_ShouldGenerateArrayInClass()
        {
            var builder = new CodeBuilder();
            builder.Write("namespace ExampleNamespace").OpenBlock();
            builder.WriteLine("public class ExampleClass").OpenBlock();
            builder.WriteLine("public int[] ExampleArray = new int[] { 1, 2, 3, 4, 5 };");
            builder.CloseAllBlocks();
            return Verify(builder.ToString(), extension: "cs");
        }

        [TestMethod]
        public Task WriteIteration_ShouldGenerate()
        {
            var builder = new CodeBuilder();
            IEnumerable<string> enumerable = ["int a = 0;", "int b = 1;", "int c = 2;", "int d = 3;", "int e = 4;"];
            builder.Write("public class ExampleClass").OpenBlock();
            builder.WriteIteration(enumerable);
            builder.CloseBlock();
            return Verify(builder.ToString(), extension: "cs");
        }

        [TestMethod]
        public Task WriteIterationEmpty_ShouldNotGenerate()
        {
            var builder = new CodeBuilder();
            IEnumerable<string> enumerable = [];
            builder.Write("public class ExampleClass").OpenBlock();
            builder.WriteIteration(enumerable);
            builder.CloseBlock();
            return Verify(builder.ToString(), extension: "cs");
        }

        [TestMethod]
        public Task CustomWriteIteration_ShouldGenerate()
        {
            var builder = new CodeBuilder();
            IEnumerable<string> enumerable = ["a = 0;", "b = 1;", "c = 2;", "d = 3;", "e = 4;"];
            builder.Write("public class ExampleClass").OpenBlock();
            builder.WriteIteration(enumerable, (builder1, item, next) =>
            {
                builder1.WriteLine("int ").Write(item);
            });
            builder.CloseBlock();
            return Verify(builder.ToString(), extension: "cs");
        }

        [TestMethod]
        public Task CustomWriteIterationEmpty_ShouldNotGenerate()
        {
            var builder = new CodeBuilder();
            IEnumerable<string> enumerable = [];
            builder.Write("public class ExampleClass").OpenBlock();
            builder.WriteIteration(enumerable, (builder1, item, next) =>
            {
                builder1.WriteLine("int ").Write(item);
            });
            builder.CloseBlock();
            return Verify(builder.ToString(), extension: "cs");
        }
    }
}