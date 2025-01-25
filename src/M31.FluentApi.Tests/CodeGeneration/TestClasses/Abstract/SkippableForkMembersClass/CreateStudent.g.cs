// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.SkippableForkMembersClass;

public class CreateStudent :
    CreateStudent.ICreateStudent,
    CreateStudent.IWithMember0,
    CreateStudent.IWithMember1AWithMember1B,
    CreateStudent.IWithMember0WithMember1AWithMember1B,
    CreateStudent.IWithMember2
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

    public static IWithMember1AWithMember1B WithMember0(string? member0)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Member0 = member0;
        return createStudent;
    }

    public static IWithMember2 WithMember1A(string? member1A)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Member1A = member1A;
        return createStudent;
    }

    public static IWithMember0 WithMember1B(string? member1B)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Member1B = member1B;
        return createStudent;
    }

    public static Student WithMember2(string? member2)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Member2 = member2;
        return createStudent.student;
    }

    IWithMember1AWithMember1B IWithMember0WithMember1AWithMember1B.WithMember0(string? member0)
    {
        student.Member0 = member0;
        return this;
    }

    IWithMember2 IWithMember0WithMember1AWithMember1B.WithMember1A(string? member1A)
    {
        student.Member1A = member1A;
        return this;
    }

    IWithMember0 IWithMember0WithMember1AWithMember1B.WithMember1B(string? member1B)
    {
        student.Member1B = member1B;
        return this;
    }

    Student IWithMember2.WithMember2(string? member2)
    {
        student.Member2 = member2;
        return student;
    }

    public interface ICreateStudent : IWithMember0
    {
    }

    public interface IWithMember0 : IWithMember0WithMember1AWithMember1B
    {
    }

    public interface IWithMember1AWithMember1B : IWithMember2, IWithMember0WithMember1AWithMember1B
    {
    }

    public interface IWithMember0WithMember1AWithMember1B
    {
        IWithMember1AWithMember1B WithMember0(string? member0);

        IWithMember2 WithMember1A(string? member1A);

        IWithMember0 WithMember1B(string? member1B);
    }

    public interface IWithMember2
    {
        Student WithMember2(string? member2);
    }
}