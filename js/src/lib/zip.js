Array.prototype.zip = function (secondArray, resultSelector) {
    const resultArray = [];
    for (let i = 0; i < this.length && i < secondArray.length; i++) {
        resultArray.push(resultSelector(this[i], secondArray[i]));
    }
    return resultArray;
}
