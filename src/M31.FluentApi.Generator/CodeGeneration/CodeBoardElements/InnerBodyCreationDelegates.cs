using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class InnerBodyCreationDelegates
{
    private readonly Dictionary<string, SetMemberCode> memberToSetMemberCode;
    private readonly Dictionary<MethodIdentity, CallMethodCode> methodToCallMethodCode;

    public InnerBodyCreationDelegates()
    {
        memberToSetMemberCode = new Dictionary<string, SetMemberCode>();
        methodToCallMethodCode = new Dictionary<MethodIdentity, CallMethodCode>();
    }

    internal void AssignSetMemberCode(string memberName, SetMemberCode setMemberCode)
    {
        if (memberToSetMemberCode.ContainsKey(memberName))
        {
            throw new GenerationException(
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
        MethodIdentity methodIdentity = CreateMethodIdentity(methodSymbolInfo);

        if (methodToCallMethodCode.ContainsKey(methodIdentity))
        {
            throw new GenerationException(
                $"{nameof(CallMethodCode)} for method {methodIdentity.MethodName} has already been assigned.");
        }

        methodToCallMethodCode[methodIdentity] = callMethodCode;
    }

    internal CallMethodCode GetCallMethodCode(MethodSymbolInfo methodSymbolInfo)
    {
        return methodToCallMethodCode[CreateMethodIdentity(methodSymbolInfo)];
    }

    private static MethodIdentity CreateMethodIdentity(MethodSymbolInfo methodSymbolInfo)
    {
        return MethodIdentity.Create(methodSymbolInfo.Name,
            methodSymbolInfo.ParameterInfos.Select(i => i.TypeForCodeGeneration));
    }
}