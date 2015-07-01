using System;

namespace EnclosedWithThanks.Enclosure
{
    public class Enclosing : IDisposable
    {
        public static T GetEnclosure<T>(Action<T> onOpen, Action<T> onClose)
            where T : Enclosing, new()
        {
            T enclosing = new T();
            enclosing.Opening = () => onOpen(enclosing);
            enclosing.Closing = () => onClose(enclosing);
            enclosing.DoOpen();
            return enclosing;
        }

        public static Enclosing GetEnclosure(Action<Enclosing> onOpen, Action<Enclosing> onClose)
        {
            return GetEnclosure<Enclosing>(onOpen, onClose);
        }

        public void Dispose()
        {
            DoClose();
        }

        protected void DoOpen()
        {
            if (Opening != null)
            {
                Opening();
            }
        }
        protected void DoClose()
        {
            if (Closing != null)
            {
                Closing();
            }
        }

        protected Action Opening { get; set; }
        protected Action Closing { get; set; }
    }
}