#region Copyright
// ---------------------------------------------------------------------------
// Copyright (c) 2024 Battleline Productions LLC. All rights reserved.
//
// Licensed under the Battleline Productions LLC license agreement.
// See LICENSE file in the project root for full license information.
//
// Author: Michael Cavanaugh
// Company: Battleline Productions LLC
// Date: 12/12/2024
// Solution Name: ConstantReminders-Api
// Project Name: ConstantReminders.Data
// File: BaseRepository.cs
// File Path: C:\git\codingyourcareer\ConstantReminders-Api\ConstantReminders.Data\BaseRepository.cs
// ---------------------------------------------------------------------------
#endregion

using ConstantReminders.Contracts.Interfaces.Data;
using ConstantReminders.Contracts.Models;

namespace ConstantReminders.Data;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
{
    public Task<TEntity> Persist(TEntity entity)
    {
        throw new NotImplementedException();
    }
}