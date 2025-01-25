// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.TwoParameterCompoundClassReversedParameters;

public class CreateStudent :
    CreateStudent.ICreateStudent,
    CreateStudent.IWithName
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

    public static Student WithName(string lastName, string firstName)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.LastName = lastName;
        createStudent.student.FirstName = firstName;
        return createStudent.student;
    }

    Student IWithName.WithName(string lastName, string firstName)
    {
        student.LastName = lastName;
        student.FirstName = firstName;
        return student;
    }

    public interface ICreateStudent : IWithName
    {
    }

    public interface IWithName
    {
        Student WithName(string lastName, string firstName);
    }
}