using NSpec;
using EnclosedWithThanks.Enclosure;

namespace EnclosedWithThanks.Spec
{
    class describe_Enclosing : nspec
    {

        class CustomEnclosing : Enclosing
        {
            private int myNumber = 0;
            public int MyNumber
            {
                get { return myNumber; }
                set { myNumber = value; }
            }
        }


        void when_opening_action_is_supplied()
        {
            it["should trigger onOpen action when opening"] = () => {
                int i = 0;
                using (
                    Enclosing.GetEnclosure(
                        onOpen: (context) => { i++; },
                        onClose: (context) => {}))
                {
                    i.should_be(1);
                }
            };

            it["should trigger onOpen action when opening within custom subclass"] = () => {
                using (
                    var enclosing = Enclosing.GetEnclosure<CustomEnclosing>(
                        onOpen: (context) => { context.MyNumber++; },
                        onClose: (context) => {}))
                {
                    enclosing.MyNumber.should_be(1);
                }
            };
        }


        void when_closing_action_is_supplied()
        {
            it["should trigger onClose action when closing"] = () => {
                int i = 0;
                using (
                    Enclosing.GetEnclosure(
                        onOpen: (context) => {},
                        onClose: (context) => { i++; }))
                {
                    i.should_be(0);
                }
                i.should_be(1);
            };

            it["should trigger onClose action when closing within custom subclass"] = () => {
                int myNumberCopied = 0;

                using (
                    var enclosing = Enclosing.GetEnclosure<CustomEnclosing>(
                        onOpen: (context) => {},
                        onClose: (context) => { context.MyNumber++; myNumberCopied = context.MyNumber; }))
                {
                    enclosing.MyNumber.should_be(0);
                }
                myNumberCopied.should_be(1);
            };
        }


        void when_opening_and_closing_actions_are_supplied()
        {
            it["should trigger both"] = () => {
                int i = 0;
                using (
                    Enclosing.GetEnclosure(
                        onOpen: (context) => { i++; },
                        onClose: (context) => { i++; }))
                {
                    i.should_be(1);
                }
                i.should_be(2);
            };

            it["should trigger both within custom subclass"] = () => {
                int myNumberCopied = 0;
                using (
                    var enclosing = Enclosing.GetEnclosure<CustomEnclosing>(
                        onOpen: (context) => { context.MyNumber++; },
                        onClose: (context) => { context.MyNumber++; myNumberCopied = context.MyNumber; }))
                {
                    enclosing.MyNumber.should_be(1);
                }
                myNumberCopied.should_be(2);
            };
        }


        void with_actions_taking_no_parameters_provided()
        {
            it["should accept and trigger"] = () => {
                int i = 0;
                using (
                    Enclosing.GetEnclosure(
                        onOpen: () => { i++; },
                        onClose: () => { i++; }))
                {
                    i.should_be(1);
                }
                i.should_be(2);
            };
        }
    }
}