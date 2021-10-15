using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using ConverterToMorseLambda;

namespace ConverterToMorseLambda.Tests
{
    public class MorseFunctionTest
    {

        [Fact]
        public void TestMorseFuctionTest()
        {
            var function = new  MorseFunction();
            ILambdaContext context = new TestLambdaContext();
            var request = new APIGatewayProxyRequest()
            {
                QueryStringParameters = new ConcurrentDictionary<string, string>()
                {
                    ["text"] = "Text To Morse Lib TDD"
                }
            };
            var result = new APIGatewayProxyResponse()
            {
                StatusCode = 200,
                Body = "-.-..-- / ---- / -----.-..... / .-....-... / --..-.."
            };
            var act = function.FunctionHandler(request, context);
            Assert.Equal(result.Body,act.Body);
        }

    }
}
