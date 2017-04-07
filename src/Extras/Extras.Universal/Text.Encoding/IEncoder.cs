﻿//-----------------------------------------------------------------------
// <copyright file="IEncoder.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace Genesys.Extras.Text.Encoding
{
    /// <summary>
    /// Encoding interface
    /// </summary>
    [CLSCompliant(true)]
    public interface IEncoder
    {
        /// <summary>
        /// Encode
        /// </summary>
        /// <returns></returns>
        string Encode();

        /// <summary>
        /// Decode
        /// </summary>
        /// <returns></returns>
        string Decode();
    }
}
