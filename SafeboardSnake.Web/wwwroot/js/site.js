// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

// variables
let serverErrorMessageBlock = document.getElementById("serverErrorMessage");
let gameBlock = document.getElementById("gameBlock");

let timeBetweenTurns;
let canvas = document.getElementById("gameboardCanvas");
let canvasContext = canvas.getContext("2d");
let turnNumberContext = document.getElementById("turnNumberTxt");

// requests
async function getGameTurnDescriptor() {
    let url = new URL('http://localhost:5000/api/gameboard');
    let response;

    try {
        response = await fetch(url,
            {
                mode: 'cors'
            });
    } catch (e) {
        serverErrorMessageBlock.hidden = false;
        gameBlock.hidden = true;
        return null;
    }

    if (!serverErrorMessageBlock.hidden) {
        serverErrorMessageBlock.hidden = true;
    }

    if (gameBlock.hidden) {
        gameBlock.hidden = false;
    }

    return await response.json();
}

async function changeDirection(directionPointer) {
    let bodyObject = {
        direction: directionPointer
    }

    let url = new URL('http://localhost:5000/api/direction');
    let response = await fetch(url,
        {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(bodyObject)
        });

    return await response.json();
}

// inner functions
async function loadPage() {
    let turnDescriptor = await getGameTurnDescriptor();

    if (turnDescriptor == null) {
        return;
    }

    timeBetweenTurns = Math.round(turnDescriptor.turnUntilNextTurnMilliseconds * 1);

    redrawView(turnDescriptor.gameboardSize, turnDescriptor.snakeCells, turnDescriptor.foodCells);
    rewriteHtmlElements(turnDescriptor.turnNumber);
}

async function loopGame() {
    let turnDescriptor = await getGameTurnDescriptor();

    if (turnDescriptor != null) {
        redrawView(turnDescriptor.gameboardSize, turnDescriptor.snakeCells, turnDescriptor.foodCells);
        rewriteHtmlElements(turnDescriptor.turnNumber);
    }

    setTimeout(loopGame(), timeBetweenTurns);
}

// launching loop and starting game here
loopGame();

function redrawView(gameBoard, snakeCells, foodCells) {
    canvas.width = gameBoard.width * 15 + 10;
    canvas.height = gameBoard.height* 15 + 10;

    // gameboard
    canvasContext.fillStyle = "black";
    for (i = 0; i < gameBoard.height; i++) {
        for (j = 0; j < gameBoard.width; j++) {
            canvasContext.fillRect(i * 14 + 2, j * 14 + 2, 10, 10);
        }
    }

    // snake
    canvasContext.fillStyle = "green";
    for (i = 0; i < snakeCells.length; i++) {
        canvasContext.fillRect(snakeCells[i].x * 14 + 2, snakeCells[i].y * 14 + 2, 10, 10);
    }

    // food
    canvasContext.fillStyle = "red";
    for (i = 0; i < foodCells.length; i++) {
        canvasContext.fillRect(foodCells[i].x * 14 + 2, foodCells[i].y * 14 + 2, 10, 10);
    }
}

function rewriteHtmlElements(turnNumber) {
    turnNumberContext.textContent = turnNumber;
}

// KEY PRESSES 
document.onkeydown = function (e) {
    switch (e.keyCode) {
        case 37:
            changeDirection('Left');
            break;
        case 38:
            changeDirection('Top');
            break;
        case 39:
            changeDirection('Right');
            break;
        case 40:
            changeDirection('Down');
            break;
    }
};