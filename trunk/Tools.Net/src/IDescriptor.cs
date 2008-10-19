namespace Tools.Core
{
    /// <summary>
    /// Provides an interface for a "description" contract, when an item can describe 
    /// itself with given name and description.
    /// </summary>
    public interface IDescriptor
    {
        /// <summary>
        /// Provides the name for the "thing" we need to describe.
        /// Very often it is also implied to be unique within the range of same types.
        /// When implementing this member of interface and the type is supposed to be serializable to xml
        /// then internal guideline is to serialize it as an attribute as opposed to the <see cref="Name"/>
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Provides the description for the "thing" we need to describe.
        /// When implementing this member of interface and the type is supposed to be serializable to xml
        /// then internal guideline is to serialize it as an element as opposed to the <see cref="Name"/>
        /// </summary>
        string Description { get; set; }
    }
}