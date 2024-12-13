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
// Project Name: ConstantReminders.Contracts
// File: IBaseRepository.cs
// File Path: C:\git\codingyourcareer\ConstantReminders-Api\ConstantReminders.Contracts\Interfaces\Data\IBaseRepository.cs
// ---------------------------------------------------------------------------
#endregion

using ConstantReminders.Contracts.Models;

namespace ConstantReminders.Contracts.Interfaces.Data;

public interface IBaseRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity> Persist(TEntity entity);
}