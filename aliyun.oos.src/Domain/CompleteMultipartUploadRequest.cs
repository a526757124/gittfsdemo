﻿/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 * 版权所有 （C）阿里云计算有限公司
 */

using System.Collections.Generic;

namespace Aliyun.OpenServices.OpenStorageService
{
    /// <summary>
    /// 指定完成Multipart Upload的请求参数。
    /// </summary>
    public class CompleteMultipartUploadRequest
    {
        private readonly IList<PartETag> _partETags = new List<PartETag>();  
        
        /// <summary>
        /// 获取或者设置<see cref="OssObject" />所在<see cref="Bucket" />的名称。
        /// </summary>
        public string BucketName { get; private set; }
        
        /// <summary>
        /// 获取或者设置<see cref="OssObject" />的值。
        /// </summary>
        public string Key { get; private set; }
        
        /// <summary>
        /// 获取或设置上传Multipart上传事件的Upload ID。
        /// </summary>
        public string UploadId { get; private set; }
        
        /// <summary>
        /// 获取或者设置标识Part上传结果的<see cref="PartETag" />对象列表。
        /// </summary>
        public IList<PartETag> PartETags
        {
            get { return _partETags; }
        } 
        
        public CompleteMultipartUploadRequest(string bucketName, string key, string uploadId)
        {
            BucketName = bucketName;
            Key = key;
            UploadId = uploadId;
        }
    }
}
