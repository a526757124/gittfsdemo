﻿/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 * 版权所有 （C）阿里云计算有限公司
 */

using System;
using System.Collections.Generic;
using Aliyun.OpenServices.Common.Communication;
using System.IO;
using Aliyun.OpenServices.OpenStorageService.Transform;
using Aliyun.OpenServices.OpenStorageService.Utilities;
using Aliyun.OpenServices.Utilities;

namespace Aliyun.OpenServices.OpenStorageService.Commands
{
    internal class SetBucketLoggingCommand : OssCommand
    {
        private readonly string _bucketName;
        private readonly SetBucketLoggingRequest _setBucketLoggingRequest;

        protected override HttpMethod Method
        {
            get { return HttpMethod.Put; }
        }

        protected override string Bucket
        {
            get { return _bucketName; }
        }

        private SetBucketLoggingCommand(IServiceClient client, Uri endpoint, ExecutionContext context,
                                    string bucketName, SetBucketLoggingRequest setBucketLoggingRequest)
            : base(client, endpoint, context)
        {
            OssUtils.CheckBucketName(setBucketLoggingRequest.BucketName);
            OssUtils.CheckBucketName(setBucketLoggingRequest.TargetBucket);

            if (!OssUtils.IsLoggingPrefixValid(setBucketLoggingRequest.TargetPrefix))
                throw new ArgumentException("Invalid logging prefix " + setBucketLoggingRequest.TargetPrefix);

            _bucketName = bucketName;
            _setBucketLoggingRequest = setBucketLoggingRequest;
        }

        public static SetBucketLoggingCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context,
                                     string bucketName, SetBucketLoggingRequest setBucketLoggingRequest)
        {
            return new SetBucketLoggingCommand(client, endpoint, context, bucketName, setBucketLoggingRequest);
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
        protected override Stream Content
        {
            get
            {
                return SerializerFactory.GetFactory().CreateSetBucketLoggingRequestSerializer()
                    .Serialize(_setBucketLoggingRequest);
            }
        }
    }
}
