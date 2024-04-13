// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using M31.FluentApi.Attributes;
using System.Reflection;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.PrivateFieldClass;

public class CreateStudent
{
    private readonly Student student;
    private static readonly FieldInfo semesterFieldInfo;

    static CreateStudent()
    {
        semesterFieldInfo = typeof(Student).GetField("semester", BindingFlags.Instance | BindingFlags.NonPublic)!;
    }

    private CreateStudent()
    {
        student = new Student();
    }

    public static Student InSemester(int semester)
    {
        CreateStudent createStudent = new CreateStudent();
        semesterFieldInfo.SetValue(createStudent.student, semester);
        return createStudent.student;
    }
}