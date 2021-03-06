﻿#region copyright
// Copyright 2015 Keith Woods
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;

namespace Esp.Net
{
    public interface IEventContext
    {
        bool IsCanceled { get; }
        void Cancel();
        void Commit();
    }

    internal class EventContext : IEventContext
    {
        private bool _isCanceled;
        private bool _isCommitted;

        public bool IsCanceled 
        {
            get { return _isCanceled; }
        }

        public bool IsCommitted
        {
            get { return _isCommitted; }
        }

        public void Cancel()
        {
            if(_isCanceled) throw new Exception("Already canceled");
            _isCanceled = true;
        }
        
        public void Commit()
        {
            if (_isCommitted) throw new Exception("Already committed");
            _isCommitted = true;
        }
    }
}