using AutoFixture.NextUnit;
using NextUnit.Autofixture.AutoMoq.Core.AttributeLogic.LogicHandlers;
using NextUnit.Core.AttributeLogic;

namespace NextUnit.Autofixture.AutoMoq.Core
{
    /// <summary>
    /// Extends the log of the AttributeLogicMapper to also suppoert AutoMoq.Autofixture features.
    /// </summary>
    public class AutofixtureAutomoqAttributeAttributeLogicMapper : AttributeLogicMapper
    {
        public AutofixtureAutomoqAttributeAttributeLogicMapper()
        {
            _mapping.Add(typeof(AutoDataAttribute), new AutoDataAttributeLogicHandler());
            
            // this is referring to a parameter attribute.
            _mapping.Add(typeof(CustomizeAttribute), new CustomizeAttributeLogicHandler());
            
            _mapping.Add(typeof(FavorArraysAttribute), new FavorArraysAttributeLogicHandler());
            _mapping.Add(typeof(FavorEnumerablesAttribute), new FavorEnumerablesAttributeLogicHandler());
            _mapping.Add(typeof(FrozenAttribute), new FrozenAttributeLogicHandler());
            _mapping.Add(typeof(InlineAutoDataAttribute), new InlineAutoDataAttributeLogicHandler());
            _mapping.Add(typeof(GreedyAttribute), new GreedyAttributeLogicHandler());
            _mapping.Add(typeof(ModestAttribute), new ModestAttributeLogicHandler());
            _mapping.Add(typeof(NoAutoPropertiesAttribute), new NoAutoPropertiesAttributeLogicHandler());
        }

        public override IAttributeLogicHandler GetHandlerFor(Attribute attribute)
        {
            if (attribute.GetType().Namespace.Contains("NextUnit.Autofixture.AutoMoq.Core"))
            {
                Type attributeType = attribute.GetType().BaseType;
                IAttributeLogicHandler attributeLogicHandler = _mapping.TryGetValue(attributeType, out var handler) ? handler : null;
                return attributeLogicHandler;
            }
            return base.GetHandlerFor(attribute);
        }
    }
}
