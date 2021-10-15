using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using TextToMorse.Lb;
// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ConverterToMorseLambda
{
    public class MorseFunction
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// prop
        public const string TextKey = "text";
        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var text = string.Empty;
            var status = 0;
            try
            {

                if (request.QueryStringParameters != null && request.QueryStringParameters.ContainsKey(TextKey))
                {
                    var code = TextToMorse.Lb.TextMorse.ConverterToMorse(request.QueryStringParameters[TextKey]);
                    text = $"Morse Code is: {code}";
                    status = 200;
                }
                else
                {
                    status = 400;
                    text = "Cannot be null";
                }
                return new APIGatewayProxyResponse()
                {
                    StatusCode = status,
                    Body = text
                };
            }
            catch (Exception e)
            {
                status = 400;
                return new APIGatewayProxyResponse()
                {
                    StatusCode = status,
                    Body = $"{e.Message}"
                };
            }

        }
    }
}
