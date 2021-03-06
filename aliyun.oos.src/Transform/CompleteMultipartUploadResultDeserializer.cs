﻿/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 * 版权所有 （C）阿里云计算有限公司
 */

using System.IO;
using Aliyun.OpenServices.Common.Communication;
using Aliyun.OpenServices.Common.Transform;
using Aliyun.OpenServices.OpenStorageService.Model;
using Aliyun.OpenServices.OpenStorageService.Utilities;

namespace Aliyun.OpenServices.OpenStorageService.Transform
{
    internal class CompleteMultipartUploadResultDeserializer 
        : ResponseDeserializer<CompleteMultipartUploadResult, CompleteMultipartUploadResultModel>
    {
        public CompleteMultipartUploadResultDeserializer(IDeserializer<Stream, CompleteMultipartUploadResultModel> contentDeserializer)
            : base(contentDeserializer)
        { }
        
        public override CompleteMultipartUploadResult Deserialize(ServiceResponse xmlStream)
        {
            var result = ContentDeserializer.Deserialize(xmlStream.Content);
            return new CompleteMultipartUploadResult
            {
                BucketName = result.Bucket,
                Key = result.Key,
                Location = result.Location,
                ETag = OssUtils.TrimQuotes(result.ETag)
            };
        }
    }
}
