using System;
using Rhino.Mocks;

namespace Tools.Tests.Helpers
{
    public static class CompositePatternTestHelper
    {
        /// <summary>
        /// Helper method to test composite [parent/child] pattern implementation, where calls
        /// to the parent result into calls onto its children.
        /// </summary>
        /// <remarks>Creates the parent object using its default constructor</remarks>
        public static void TestForCompositeOperation<ParentType, ChildType>(Action<ParentType> parentAction, Action<ChildType> childAction, Action<ParentType, ChildType> addChild)
            where ChildType : class
            where ParentType : new()
        {
            // Requires a default ctor to exists
            var parent = new ParentType();

            TestForCompositeOperation(parent, parentAction, childAction, addChild);
        }
        /// <summary>
        /// Helper method to test composite [parent/child] pattern implementation, where calls
        /// to the parent result into calls onto its children.
        /// </summary>
        /// <remarks>Uses the passed in instance of a parent</remarks>
        public static void TestForCompositeOperation<ParentType, ChildType>(ParentType parent, 
            Action<ParentType> parentAction, Action<ChildType> childAction, Action<ParentType, ChildType> addChild)
            where ChildType : class
        {
            // Use Rhino.Mocks to create stubs
            var child1 = MockRepository.GenerateStub<ChildType>();
            var child2 = MockRepository.GenerateStub<ChildType>();
            // Setup two children, the arbitrary choice, but should not really matter
            child1.Expect(childAction);
            child2.Expect(childAction);
            // Add children to the parent
            addChild(parent, child1);
            addChild(parent, child2);
            // Call parent action
            parentAction(parent);
            // Assert parent action resulted in the calls to children
            child1.AssertWasCalled(childAction);
            child2.AssertWasCalled(childAction);
        }
        public static void TestForCompositeOperation<ParentType, ChildType, ChildCallResultType>(ParentType parent,
            Action<ParentType> parentAction, Func<ChildType, ChildCallResultType> childAction, Action<ParentType, ChildType> addChild)
            where ChildType : class
            where ChildCallResultType : new()
        {
            //MockRepository mocks = new MockRepository();
            //// Use Rhino.Mocks to create stubs
            //var child1 = mocks..StrictMock<ChildType>();
            //var child2 = mocks.StrictMock<ChildType>();

            //using (mocks.Record())
            //{
            //    Expect.Call(childAction).;
            //}
            //// Add children to the parent
            //addChild(parent, child1);
            //addChild(parent, child2);
            //// Call parent action
            //using (mocks.Playback())
            //{
            //    parentAction(parent);
            //}
            //mocks.VerifyAll();
            // Use Rhino.Mocks to create stubs
            var child1 = MockRepository.GenerateStub<ChildType>();
            var child2 = MockRepository.GenerateStub<ChildType>();
            // Setup two children, the arbitrary choice, but should not really matter
            child1.Expect(childAction).IgnoreArguments().Return(new ChildCallResultType());
            child2.Expect(childAction).IgnoreArguments().Return(new ChildCallResultType());
            // Add children to the parent
            addChild(parent, child1);
            addChild(parent, child2);
            // Call parent action
            parentAction(parent);
            // Assert parent action resulted in the calls to children
            child1.VerifyAllExpectations();
            child2.VerifyAllExpectations();
        }
    }
}
