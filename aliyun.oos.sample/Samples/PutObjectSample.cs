/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 * 版权所有 （C）阿里云计算有限公司
 */

using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Aliyun.OpenServices.OpenStorageService.Samples
{
    /// <summary>
    /// Sample for putting object.
    /// </summary>
    public static class PutObjectSample
    {
        const string accessId = "k7Y9gdM8FkpjkeNk";
        const string accessKey = "p6CoJlZkB7ZOJPysjrTOh6DACBZjRA";
        const string endpoint = "http://oss-cn-qingdao.aliyuncs.com";

        static OssClient client = new OssClient(endpoint, accessId, accessKey);

        const string bucketName = "cdtest";
        const string key = "img/11.jpg";
        const string fileToUpload = "D:\\1.jpg";

        static AutoResetEvent _evnet = new AutoResetEvent(false);

        public static void PutObject()
        {
            try
            {

                string str = "a line of simple text";
                byte[] binaryData = Encoding.ASCII.GetBytes(str);
                MemoryStream requestContent = new MemoryStream(binaryData);
                client.PutObject(bucketName, key, requestContent);

                // 1. put object to specified output stream
                using (var fs = File.Open(fileToUpload, FileMode.Open))
                {
                    var metadata = new ObjectMetadata();
                    metadata.CacheControl = "No-Cache";
                    metadata.ContentLength = fs.Length;
                    var result= client.PutObject(bucketName, key, fs, metadata);
                    Console.WriteLine(key + " put done.");
                }
                var o = client.GetObject(bucketName, key);
                using (var requestStream = o.Content)
                {
                    int len = (int)o.Metadata.ContentLength;
                    var buf = new byte[len];
                    requestStream.Read(buf, 0, len);
                    var fs = File.Open("E:", FileMode.OpenOrCreate);
                    fs.Write(buf, 0, len);
                    //eTag = ComputeContentMd5(requestStream);
                    //Assert.AreEqual(o.Metadata.ETag, eTag.ToUpper());
                }
                // 2. put object to specified file
                //client.PutObject(bucketName, key, fileToUpload);

                // 3. put object from specified object with multi-level virtual directory
                //key = "folder/sub_folder/key0";
                //client.PutObject(bucketName, key, fileToUpload);

            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}", 
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }
        }

        public static void AsyncPutObject()
        {
            try
            {
                // 1. put object to specified output stream
                var fs = File.Open(fileToUpload, FileMode.Open);
                var metadata = new ObjectMetadata();
                metadata.UserMetadata.Add("mykey1", "myval1");
                metadata.UserMetadata.Add("mykey2", "myval2");
                metadata.CacheControl = "No-Cache";
                metadata.ContentType = "text/html";
                client.BeginPutObject(bucketName, key, fs, metadata, PutObjectCallback, new string('a', 8));

                _evnet.WaitOne();
                fs.Close();
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }
        }

        private static void PutObjectCallback(IAsyncResult ar)
        {
            try
            {
                var result = client.EndPutObject(ar);
                Console.WriteLine(result.ETag);
            }
            catch (Exception ex)
            {
                OssException ae = ex as OssException;
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ae.ErrorCode, ae.Message, ae.RequestId, ae.HostId);
            }
            finally
            {
                _evnet.Set();
            }
        }
    }
}
