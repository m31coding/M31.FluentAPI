// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#nullable enable

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.PredicateClass;

public class CreateStudent
{
    private readonly Student student;

    private CreateStudent()
    {
        student = new Student();
    }

    public static Student WhoIsHappy(bool isHappy = true)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.IsHappy = isHappy;
        return createStudent.student;
    }

    public static Student WhoIsSad()
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.IsHappy = false;
        return createStudent.student;
    }
}