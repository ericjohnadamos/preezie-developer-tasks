﻿namespace Preezie.WebAPI.Models;

using Preezie.Application.Commons.Dtos;

public record GetTodoItemsResponse(IEnumerable<TodoItemDto> TodoItems);
public record GetTodoItemByIdResponse(TodoItemDto TodoItem);

