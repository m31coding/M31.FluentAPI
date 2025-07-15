using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class InnerBodyCreationDelegates
{
    private readonly Dictionary<string, SetMemberCode> memberToSetMemberCode;
    private readonly Dictionary<MethodSymbolInfo, CallMethodCode> methodToCallMethodCode;

    public InnerBodyCreationDelegates()
    {
        memberToSetMemberCode = new Dictionary<string, SetMemberCode>();
        methodToCallMethodCode = new Dictionary<MethodSymbolInfo, CallMethodCode>();
    }

    internal void AssignSetMemberCode(string memberName, SetMemberCode setMemberCode)
    {
        if (memberToSetMemberCode.ContainsKey(memberName))
        {
            throw new InvalidOperationException(
                $"{nameof(SetMemberCode)} for member {memberName} has already been assigned.");
        }

        memberToSetMemberCode[memberName] = setMemberCode;
    }

    internal SetMemberCode GetSetMemberCode(string memberName)
    {
        return memberToSetMemberCode[memberName];
    }

    internal void AssignCallMethodCode(MethodSymbolInfo methodSymbolInfo, CallMethodCode callMethodCode)
    {
        if (methodToCallMethodCode.ContainsKey(methodSymbolInfo))
        {
            throw new InvalidOperationException(
                $"{nameof(CallMethodCode)} for method {methodSymbolInfo.Name} has already been assigned.");
        }

        methodToCallMethodCode[methodSymbolInfo] = callMethodCode;
    }

    internal CallMethodCode GetCallMethodCode(MethodSymbolInfo methodSymbolInfo)
    {
        return methodToCallMethodCode[methodSymbolInfo];
    }
}