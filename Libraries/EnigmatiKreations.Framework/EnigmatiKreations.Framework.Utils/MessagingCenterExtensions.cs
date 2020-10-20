using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EnigmatiKreations.Framework.Utils
{
    public static class MessagingCenterExtensions
    {
        private readonly static HashSet<SubscriptionKey> SubscriptionSet
            = new HashSet<SubscriptionKey>();

        public static void UnitarySubscribe<TSender, TArgs, TReceiver>(object subscriber, string msg, Action<TSender, TArgs> callback) 
            where TSender : class
        {
            var key = SubscriptionKey.NewInstance<TSender, TReceiver>(msg);

            if (SubscriptionSet.Contains(key)) {
                return;
            }
            MessagingCenter.Subscribe(subscriber, msg, callback);
            SubscriptionSet.Add(key);
        }
    }

    internal class SubscriptionKey : IEquatable<SubscriptionKey> 
    {
        private Type _SenderType;
        private Type _ReceiverType;
        private string _Message;

        private SubscriptionKey(Type senderType, Type receiverType, string msg)
        {
            _SenderType = senderType;
            _ReceiverType = receiverType;
            _Message = msg;
        }

        public static SubscriptionKey NewInstance<TSender, TReceiver>(string msg) where TSender: class {
            return new SubscriptionKey(typeof(TSender), typeof(TReceiver), msg);
        }

        public bool Equals(SubscriptionKey other)
        {
            if (other == null) return false;
            if (!_Message.Equals(other._Message)) return false;
            if (!_SenderType.Equals(other._SenderType)) return false;
            if (!_ReceiverType.Equals(other._ReceiverType)) return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == this) return true;
            if (obj == null) return false;
            if (obj is SubscriptionKey subsKey){
                return Equals(subsKey);
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 1891620547;
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(_SenderType);
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(_ReceiverType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_Message);
            return hashCode;
        }
    }
}
