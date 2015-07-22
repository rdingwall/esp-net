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

#if ESP_LOCAL
// ReSharper disable once CheckNamespace
namespace System.Reactive.Linq
{
    internal class EspObserver<T> : IObserver<T>
    {
        private readonly Action<T> _observer;
        private readonly Action<Exception> _onError;
        private readonly Action _onCompleted;
        private bool _hasError;
        private bool _isComplted;

        public EspObserver(Action<T> observer)
            : this(observer, null, null)
        {
            _observer = observer;
        }

        public EspObserver(Action<T> observer, Action<Exception> onError)
            : this(observer, onError, null)
        {
        }

        public EspObserver(Action<T> observer, Action<Exception> onError, Action onCompleted)
        {
            _observer = observer;
            _onError = onError;
            _onCompleted = onCompleted;
        }

        public void OnNext(T value)
        {
            if (_isComplted || _hasError) return;
            _observer(value);
        }

        public void OnError(Exception error)
        {
            if (_isComplted || _hasError) return;
            _hasError = true;
            if (_onError != null) 
                _onError(error);
            else
                throw error;
        }

        public void OnCompleted()
        {
            if (_isComplted || _hasError) return;
            _isComplted = true;
            if (_onCompleted != null) _onCompleted();
        }
    }
}
#endif