using System;
using System.Web;

namespace EvolvingAway
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello DotNet Oxford!");

            var i = 10;
            Console.WriteLine($"\"{nameof(i)}\" is {(i.GetType().IsPrimitive ? "Primitive" : "Not Primitive")}");
            //And the same for Boolean, Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, IntPtr, UIntPtr, Char, Double, and Single.

            //Opps - username and password wrong way around
            AddUser("daniel.bryars@speik.com", "I love dotnet!");

            var tenantId = new TenantId(new Guid("ed5c3c3d-8f7c-442a-9d70-2c7331a9b3c5"));
            var packId = new PackId(new Guid("cc7f5a9e-6709-4edf-b97c-2cc5da351c63"));

            ReleaseTheHounds(tenantId, packId);

            

        }

        //Most of the types we want to evolve are sealed or value types which we can't inherit from
        //public struct MyId : long {} //CS0827 value type
        //public class Username : string {} //CS0509 sealed type
        //public class TenantId : Guid { } //CS0509 sealed type

        static void AddUser(string password, string username)
        {
            Console.WriteLine($"User Added with username:'{username}' and password:'{password}'");
        }

        static void ReleaseTheHounds(Guid tenantId, Guid packId)
        { 
            //Easy to get wrong
        }

        public record TenantId (Guid Value);
        public record PackId(Guid Value);

        static void ReleaseTheHounds(TenantId tenantId, PackId packId)
        {
            //Compiler checks we've got the right parameters
        }

        static string HtmlEncode(string value)
        { 
            return HttpUtility.HtmlEncode(value);
        }

        public string RenderHtml(string someUserInput)
        {
            //Do lots of stuff with text
            //it got encoded here:
            someUserInput = HtmlEncode(someUserInput);

            //opps it got encoded twice
            return "<b>" +  HtmlEncode(someUserInput) + "</b>";
        }

        public record UnsafeUserInput(string value);
        public class EscapedSafeString
        {
            public EscapedSafeString(UnsafeUserInput value)
            {
                Value = HttpUtility.HtmlEncode(value.value);
            }

            public string Value { get; init; }
        }

        static string HtmlEncode(string value)
        {
            return HttpUtility.HtmlEncode(value);
        }

        public string RenderHtml(UnsafeUserInput someUserInput)
        {
            //Do lots of stuff with text
            //it got encoded here:
            someUserInput = HtmlEncode(someUserInput);

            //opps it got encoded twice
            return "<b>" + HtmlEncode(someUserInput) + "</b>";
        }

    }
}
