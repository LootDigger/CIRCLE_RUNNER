# CIRCLE_RUNNER

Для ревью:
1. В коде используется Camera.main. В нормальных условиях я бы вынес ссылку камеры в отдельный класс getter.
2. Заметил, что на больших скоростях у игрока наблюдается jittering. Видимо моё решение не было лучшим, но уже не было времени менять и переписывать.
3. UniRX я использую впервые, поэтому могу не знать стандартов использования. Однако что-то в любом случае вышло ;)
4. Думал о том, чтобы написать пару UNIT тестов и старался писать логику атомизировано, соблюдая Single responsibility для упрощения тестирования
5. В целом тестовое сделано достаточно быстро, хотя под конец из-за дедлайнов разленился и начал сильно упрощать код.

Запуск игры через сцену **GameScene**

Тестирую игру в режиме симулятора. Игра использует мобильный инпут:

![image](https://github.com/user-attachments/assets/cc8541a2-59dc-4555-8c75-f180ea4bd907) 



Настройки игры можно найти тут:

![image](https://github.com/user-attachments/assets/92d07777-1273-4c21-b00c-708e84c3838b)

