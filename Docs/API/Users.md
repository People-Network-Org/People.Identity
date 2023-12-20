# Users

## Collection

Получить массив пользователей по их Id (придут дополнительные поля, если авторизованный пользователь имеет право управления организацей)

**GET**: /api/users/collection

**Query params**:

```url
ids: string[]
```

**Result**:

Нет права управления организацей

```json
{
  "items": [
    "id": "string",
    "firstName": "string",
    "lastName": "string | undefined",
    "nickName": "string",
    "email": "string",
    "phone": "string | undefined"
  ]
}
```

Есть право управления организацей

```json
{
  "items": [
    "id": "string",
    "firstName": "string",
    "lastName": "string | undefined",
    "nickName": "string",
    "email": "string",
    "phone": "string | undefined",
    "isConfirmed": "bool",
    "emailCode": "string | undefined"
  ]
}
```

## Delete

Удалить пользователя (нужно право управления организацией)

**DELETE**: /api/users/{id:string}

**Result**:

```json
{
  "id": "string",
  "firstName": "string",
  "lastName": "string | undefined",
  "nickName": "string",
  "email": "string",
  "phone": "string | undefined"
}
```
