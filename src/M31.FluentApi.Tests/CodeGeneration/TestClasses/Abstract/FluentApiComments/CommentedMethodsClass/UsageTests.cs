#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using System;
using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentApiComments.CommentedMethodsClass;

public class UsageTests
{
     [Fact, Priority(1)]
     public void CanExecuteCommentedMethodsClass()
     {
         var student =
             CreateStudent
                 .WithName("Alice", "King")
                 .BornOn(new DateOnly(2004, 9, 25));

         Assert.Equal("Alice", student.FirstName);
         Assert.Equal("King", student.LastName);
         Assert.Equal(20, student.Age);
     }
}

#endif