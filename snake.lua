local socket = require("socket")

local width, height = 20, 10
local snake = { {x = 5, y = 5} }
local direction = {x = 1, y = 0}
local food = {x = math.random(1, width), y = math.random(1, height)}
local gameOver = false

local function clearScreen()
    os.execute("cls")
end

local function draw()
    clearScreen()
    for y = 1, height do
        for x = 1, width do
            local isSnake = false
            for _, segment in ipairs(snake) do
                if segment.x == x and segment.y == y then
                    isSnake = true
                    break
                end
            end
            if isSnake then
                io.write("O")
            elseif food.x == x and food.y == y then
                io.write("X")
            else
                io.write(".")
            end
        end
        io.write("\n")
    end
end

local function update()
    local newHead = {x = snake[1].x + direction.x, y = snake[1].y + direction.y}
    
    if newHead.x < 1 or newHead.x > width or newHead.y < 1 or newHead.y > height then
        gameOver = true
        return
    end

    for _, segment in ipairs(snake) do
        if newHead.x == segment.x and newHead.y == segment.y then
            gameOver = true
            return
        end
    end

    table.insert(snake, 1, newHead)

    if newHead.x == food.x and newHead.y == food.y then
        food = {x = math.random(1, width), y = math.random(1, height)}
    else
        table.remove(snake)
    end
end

local function changeDirection(newDirection)
    if (newDirection.x == -direction.x and newDirection.y == -direction.y) then
        return
    end
    direction = newDirection
end

while not gameOver do
    draw()
    update()
    
    local input = io.read()
    if input == "w" then
        changeDirection({x = 0, y = -1})
    elseif input == "s" then
        changeDirection({x = 0, y = 1})
    elseif input == "a" then
        changeDirection({x = -1, y = 0})
    elseif input == "d" then
        changeDirection({x = 1, y = 0})
    end
    
    socket.sleep(0.1)
end

print("Game Over!")
