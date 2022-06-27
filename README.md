# HRU
Программа написана на языке C#.
Программа выполнена как приложение для Windows, а не как консольное приложение.

Отсутсвует регистрация начального пользователя, регистрация пользователя с паролем, запись хеш-пароля.

При создании субъектов автоматически создаются объекты с такими же именами, как и у субъектов.
Есть возможность внесения/удаления прав доступа, субъекта и объекта. Есть возможность проверки системы на безопасность.

# Алгоритм:
- Нажимаем "создать команду"
- Создаем субъект
- (автоматически создается объект) можно дополнительно создать отдельные объекты (для доп.объектов создаем новую команду)
- Нажимаем "выполнить команды" (появляется табличка с субъектами и объектами)
- Для выдачи прав есть три поля ввода:
  - первое поле: пишем какое право выдаем
  - второе поле: пишем субъект
  - третье поле: пишем объект

АНАЛОГИЧНО для удаления прав доступа, объектов и субъектов

# Проверка системы
После выдачи прав, есть возможность проверки системы на безопасность (кнопка "проверить систему на безопасность")
