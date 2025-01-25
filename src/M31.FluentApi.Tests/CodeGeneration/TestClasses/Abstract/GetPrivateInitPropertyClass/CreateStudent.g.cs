// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using System.Reflection;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.GetPrivateInitPropertyClass;

public class CreateStudent :
    CreateStudent.ICreateStudent,
    CreateStudent.IInSemester
{
    private readonly Student student;
    private static readonly PropertyInfo semesterPropertyInfo;

    static CreateStudent()
    {
        semesterPropertyInfo = typeof(Student).GetProperty("Semester", BindingFlags.Instance | BindingFlags.Public)!;
    }

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
        CreateStudent.semesterPropertyInfo.SetValue(createStudent.student, semester);
        return createStudent.student;
    }

    Student IInSemester.InSemester(int semester)
    {
        CreateStudent.semesterPropertyInfo.SetValue(student, semester);
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