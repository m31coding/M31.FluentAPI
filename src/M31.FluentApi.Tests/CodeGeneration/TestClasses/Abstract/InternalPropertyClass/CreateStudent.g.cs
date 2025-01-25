// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.InternalPropertyClass;

public class CreateStudent :
    CreateStudent.ICreateStudent,
    CreateStudent.IInSemester
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

    public static Student InSemester(int semester)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Semester = semester;
        return createStudent.student;
    }

    Student IInSemester.InSemester(int semester)
    {
        student.Semester = semester;
        return student;
    }

    public interface ICreateStudent : IInSemester
    {
    }

    public interface IInSemester
    {
        Student InSemester(int semester);
    }
}