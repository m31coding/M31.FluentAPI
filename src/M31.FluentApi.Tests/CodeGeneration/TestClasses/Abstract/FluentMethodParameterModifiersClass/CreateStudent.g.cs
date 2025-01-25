// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentMethodParameterModifiersClass;

public class CreateStudent :
    CreateStudent.ICreateStudent,
    CreateStudent.IMethodWithParams,
    CreateStudent.IMethodWithRefParameter,
    CreateStudent.IMethodWithInParameter,
    CreateStudent.IMethodWithOutParameter,
    CreateStudent.IMethodWithRefInAndOutParameter
{
    private readonly Student student;

    private CreateStudent()
    {
        student = new Student();
    }

    public static ICreateStudent InitialStep()
    {
        return new CreateStudent();
    }

    public static IMethodWithRefParameter MethodWithParams(params int[] numbers)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.MethodWithParams(numbers);
        return createStudent;
    }

    IMethodWithRefParameter IMethodWithParams.MethodWithParams(params int[] numbers)
    {
        student.MethodWithParams(numbers);
        return this;
    }

    IMethodWithInParameter IMethodWithRefParameter.MethodWithRefParameter(ref int n1)
    {
        student.MethodWithRefParameter(ref n1);
        return this;
    }

    IMethodWithOutParameter IMethodWithInParameter.MethodWithInParameter(in int n2)
    {
        student.MethodWithInParameter(in n2);
        return this;
    }

    IMethodWithRefInAndOutParameter IMethodWithOutParameter.MethodWithOutParameter(out int n3)
    {
        student.MethodWithOutParameter(out n3);
        return this;
    }

    Student IMethodWithRefInAndOutParameter.MethodWithRefInAndOutParameter(ref int n4, in int n5, out int n6)
    {
        student.MethodWithRefInAndOutParameter(ref n4, in n5, out n6);
        return student;
    }

    public interface ICreateStudent : IMethodWithParams
    {
    }

    public interface IMethodWithParams
    {
        IMethodWithRefParameter MethodWithParams(params int[] numbers);
    }

    public interface IMethodWithRefParameter
    {
        IMethodWithInParameter MethodWithRefParameter(ref int n1);
    }

    public interface IMethodWithInParameter
    {
        IMethodWithOutParameter MethodWithInParameter(in int n2);
    }

    public interface IMethodWithOutParameter
    {
        IMethodWithRefInAndOutParameter MethodWithOutParameter(out int n3);
    }

    public interface IMethodWithRefInAndOutParameter
    {
        Student MethodWithRefInAndOutParameter(ref int n4, in int n5, out int n6);
    }
}