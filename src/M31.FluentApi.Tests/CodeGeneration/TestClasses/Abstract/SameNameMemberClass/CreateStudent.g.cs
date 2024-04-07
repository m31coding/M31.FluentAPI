// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#nullable enable

using M31.FluentApi.Attributes;
using System.Reflection;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.SameNameMemberClass;

public class CreateStudent : CreateStudent.IWithName, CreateStudent.IWithName2
{
    private readonly Student student;
    private static readonly PropertyInfo semesterPropertyInfo;
    private static readonly PropertyInfo initialPropertyInfo;
    private static readonly PropertyInfo lastNamePropertyInfo;

    static CreateStudent()
    {
        semesterPropertyInfo = typeof(Student).GetProperty("Semester", BindingFlags.Instance | BindingFlags.Public)!;
        initialPropertyInfo = typeof(Student).GetProperty("Initial", BindingFlags.Instance | BindingFlags.Public)!;
        lastNamePropertyInfo = typeof(Student).GetProperty("LastName", BindingFlags.Instance | BindingFlags.Public)!;
    }

    private CreateStudent()
    {
        student = new Student();
    }

    public static IWithName InSemester(int semester)
    {
        CreateStudent createStudent = new CreateStudent();
        semesterPropertyInfo.SetValue(createStudent.student, semester);
        return createStudent;
    }

    public IWithName2 WithName(char initial)
    {
        initialPropertyInfo.SetValue(student, initial);
        return this;
    }

    public Student WithName(string lastName)
    {
        lastNamePropertyInfo.SetValue(student, lastName);
        return student;
    }

    public interface IWithName
    {
        IWithName2 WithName(char initial);
    }

    public interface IWithName2
    {
        Student WithName(string lastName);
    }
}