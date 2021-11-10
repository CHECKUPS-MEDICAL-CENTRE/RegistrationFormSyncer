using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;
using ZidiWCFService;

namespace RegistrationFormSyncer
{
    public class WCFConnect
    {

        public ZidiIService GetWcfConnection(Binding binding)
        {
            EndpointAddress address = new EndpointAddress("http://197.248.142.25:8004//zidiwcfservicelibraryNew250//ZidiWCFServiceLibrary.ZidiService.svc");

            //EndpointAddress address = new EndpointAddress("http://192.168.0.13//zidiwcfservicelibraryNew203//ZidiWCFServiceLibrary.ZidiService.svc");

            //EndpointAddress address = new EndpointAddress("http://192.168.10.245//zidiwcfservicelibraryNew196//ZidiWCFServiceLibrary.ZidiService.svc");

            //EndpointAddress address = new EndpointAddress("http://192.168.88.64//zidiwcfservicelibraryNew205//ZidiWCFServiceLibrary.ZidiService.svc");

            //EndpointAddress address = new EndpointAddress("http://192.168.1.13//zidiwcfservicelibraryNew206//ZidiWCFServiceLibrary.ZidiService.svc");

            //EndpointAddress address = new EndpointAddress("http://192.168.1.62:8807//zidiwcfservicelibraryNew181/ZidiWCFServiceLibrary.ZidiService.svc");

            //EndpointAddress address = new EndpointAddress("http://192.168.100.6//zidiwcfservicelibraryNew212//ZidiWCFServiceLibrary.ZidiService.svc");

            //EndpointAddress address = new EndpointAddress("http://192.168.254.121//zidiwcfservicelibraryNew206//ZidiWCFServiceLibrary.ZidiService.svc");

            //EndpointAddress address = new EndpointAddress("http://192.168.1.50//zidiwcfservicelibraryNew207//ZidiWCFServiceLibrary.ZidiService.svc");

            ChannelFactory<ZidiIService> factory = new ChannelFactory<ZidiIService>(binding, address);

            ZidiIService channel = factory.CreateChannel();

            return channel;
        }

        public Binding HttpBinding()
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Security.Mode = BasicHttpSecurityMode.None;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            binding.TransferMode = TransferMode.Streamed;
            binding.MaxBufferPoolSize = int.MaxValue;
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.MaxBufferSize = int.MaxValue;
            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            binding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
            binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
            binding.OpenTimeout = new TimeSpan(0, 20, 0);
            binding.CloseTimeout = new TimeSpan(0, 20, 0);
            binding.SendTimeout = new TimeSpan(0, 20, 0);
            binding.ReceiveTimeout = new TimeSpan(0, 20, 0);

            return binding;
        }

        //public Binding NetTcpBinding()
        //{
        //    //NetHttpBinding binding = new NetHttpBinding();
        //    //binding.Security.Mode =SecurityMode.None;
        //    //binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
        //    //binding.TransferMode = TransferMode.Streamed;
        //    //binding.MaxBufferPoolSize = int.MaxValue;
        //    //binding.MaxReceivedMessageSize = int.MaxValue;
        //    //binding.MaxBufferSize = int.MaxValue;
        //    //binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
        //    //binding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
        //    //binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
        //    //binding.OpenTimeout = new TimeSpan(0, 10, 0);
        //    //binding.CloseTimeout = new TimeSpan(0, 10, 0);
        //    //binding.SendTimeout = new TimeSpan(0, 10, 0);
        //    //binding.ReceiveTimeout = new TimeSpan(0, 10, 0);

        //    //return binding;
        //}
    }
}
