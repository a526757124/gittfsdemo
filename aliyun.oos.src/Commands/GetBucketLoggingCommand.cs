﻿/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 * 版权所有 （C）阿里云计算有限公司
 */

using System;
using System.Collections.Generic;
using Aliyun.OpenServices.Common.Communication;
using Aliyun.OpenServices.Common.Transform;
using Aliyun.OpenServices.OpenStorageService.Transform;
using Aliyun.OpenServices.OpenStorageService.Utilities;
using Aliyun.OpenServices.Utilities;

namespace Aliyun.OpenServices.OpenStorageService.Commands
{
    internal class GetBucketLoggingCommand : OssCommand<BucketLoggingResult>
    {
        private readonly string _bucketName;

        protected override string Bucket
        {
            get { return _bucketName; }
        }

        private GetBucketLoggingCommand(IServiceClient client, Uri endpoint, ExecutionContext context,
                                    string bucketName, IDeserializer<ServiceResponse, BucketLoggingResult> deserializer)
            : base(client, endpoint, context, deserializer)
        {
            OssUtils.CheckBucketName(bucketName);
            _bucketName = bucketName;
        }

        public static GetBucketLoggingCommand Create(IServiceClient client, Uri endpoint,
                                                 ExecutionContext context,
                                                 string bucketName)
        {
            return new GetBucketLoggingCommand(client, endpoint, context, bucketName,
                                           DeserializerFactory.GetFactory().CreateGetBucketLoggingResultDeserializer());
        }

        protected override IDictionary<string, string> Parameters
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { RequestParameters.SUBRESOURCE_LOGGING, null }
                };
            }
        }
    }
}
