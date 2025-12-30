#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using System;
using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentApiComments.CommentedPropertiesClassAdvanced;

public class UsageTests
{
     [Fact, Priority(1)]
     public void CanExecuteCommentedPropertiesClassAdvanced()
     {
         var student =
             CreateStudent
                 .WithName("Alice")
                 .OfAge(22)
                 .LivingInBoston()
                 .WhoIsHappy();

         Assert.Equal("Alice", student.Name);
         Assert.Equal(22, student.Age);
         Assert.Equal("Boston", student.City);
         Assert.True(student.IsHappy);
     }
}

#endif