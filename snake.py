import random
import os
import time
import msvcrt

width, height = 20, 10
snake = [(5, 5)]
direction = (1, 0)
food = (random.randint(1, width), random.randint(1, height))
game_over = False

def clear_screen():
    os.system('cls' if os.name == 'nt' else 'clear')

def draw():
    clear_screen()
    for y in range(1, height + 1):
        for x in range(1, width + 1):
            if (x, y) in snake:
                print("O", end="")
            elif food == (x, y):
                print("X", end="")
            else:
                print(".", end="")
        print()

def update():
    global game_over, food
    new_head = (snake[0][0] + direction[0], snake[0][1] + direction[1])

    if (new_head[0] < 1 or new_head[0] > width or
        new_head[1] < 1 or new_head[1] > height or
        new_head in snake):
        game_over = True
        return

    snake.insert(0, new_head)

    if new_head == food:
        food = (random.randint(1, width), random.randint(1, height))
    else:
        snake.pop()

def change_direction(new_direction):
    global direction
    if (new_direction[0] == -direction[0] and
            new_direction[1] == -direction[1]):
        return
    direction = new_direction

while not game_over:
    draw()
    update()

    if msvcrt.kbhit():
        key = msvcrt.getch()
        if key == b'w':
            change_direction((0, -1))
        elif key == b's':
            change_direction((0, 1))
        elif key == b'a':
            change_direction((-1, 0))
        elif key == b'd':
            change_direction((1, 0))

    time.sleep(0.1)

print("Game Over!")
