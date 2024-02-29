namespace AutoFixture.NextUnit
{
    [Flags]
    public enum Matching
    {
        ExactType = 1,
        DirectBaseType = 2,
        ImplementedInterfaces = 4,
        ParameterName = 8,
        PropertyName = 0x10,
        FieldName = 0x20,
        MemberName = 0x38
    }
}
