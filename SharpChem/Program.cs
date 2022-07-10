/* File:        Program.cs
 * Project:     SharpChem
 * Author:      Elijah Creed Fedele
 * Date:        July 9, 2022
 * Description: Implements the top-level logic of the SharpChem program. As the SharpChem implementation is designed
 * to be both a library and a freestanding program, this entry point is designed to provide the forward (user-facing)
 * element, similar to the textual input of the original CEA utility.
 * 
 * Licensed to the Apache Software Foundation (ASF) under one or more contributor license agreements.  See the NOTICE 
 * file distributed with this work for additional information regarding copyright ownership.  The ASF licenses this 
 * file to you under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance 
 * with the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on 
 * an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License for the 
 * specific language governing permissions and limitations under the License. */

using System;
using System.Collections.Generic;

namespace SharpChem
{
    internal class Program
    {
        public static void Main (string[] args)
        {
            Console.WriteLine("Hello, world!");
        } 
    }
}