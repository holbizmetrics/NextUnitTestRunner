namespace NextUnit.Core.Extensions
{
    public static class AttributeExtensions
    {
        /// <summary>
        /// Use this to check if the attributes all together match the types specified.
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static bool AnyIsOf(this IEnumerable<Attribute> attributes, params Type[] types)
        {
            return types.All(type => attributes.Any(attr => type.IsInstanceOfType(attr)));
        }
    }
}
