/**
 * The let global variables storing values for the application
 */

let userCashAmount = 0;
let outstandingAmount = 0;
let workingEarned = 0;

/**
 * Const global variables
 */

//Computer array containing all computers
const computerArray = [];
//The url for the api used to collect laptop information
const defaultAssetUrl = "https://noroff-komputer-store-api.herokuapp.com/";

/**
 * The elements Id's from the html file
 */
const loanButtonEL = document.getElementById("getLoanButton");
const outstandingLoanEL = document.getElementById("outstandingLoanInfo");
const outstandingLoanAmountEL = document.getElementById(
  "outstandingLoanAmount"
);
const bankButtonEL = document.getElementById("bankButton");
const workButtonEL = document.getElementById("workButton");
const workEarnedAmountEL = document.getElementById("workEarnedAmount");
const balanceAmountEL = document.getElementById("balanceAmount");
const repayLoanButtonEL = document.getElementById("repayLoanButton");
const selectComputerEl = document.getElementById("selectComputer");
const featureDescEl = document.getElementById("featureDesc");
const computerDescEl = document.getElementById("computerDesc");
const computerTitleEl = document.getElementById("computerTitle");
const computerImgEl = document.getElementById("computerImg");
const computerPriceEl = document.getElementById("computerPrice");
const buyNowButtonEl = document.getElementById("buyNowButton");
const computerInfoEl = document.getElementById("computerInfo");

/**
 * Gets the computer api response to populate an array and sends the array to a function
 */
const computers = async () => {
  fetch("https://noroff-komputer-store-api.herokuapp.com/computers")
    .then((response) => response.json())
    .then((data) => {
      computerArray.push(data);
      accessData(computerArray);
    });
};

computers();

/**
 * Accesses the data from array and sends it to a function
 * @param {any[]} Array array with objects
 */
function accessData(Array) {
  for (let object of Array) {
    for (let items of object) {
      if (items.active) createSelectChild(items);
    }
  }
}

/**
 * Function that loops through an array, filters and runs a method to find selected value
 */

function AccessSelectedData() {
  const selectedValue =
    selectComputerEl.options[selectComputerEl.selectedIndex].value;

  const mySelectedItem = computerArray[0].filter((el) => {
    return el.title === selectedValue;
  });
  getSelectedValues(...mySelectedItem);
}
/**
 * Deconstructs an object as parameter and populates the website element id's with its values
 */
function getSelectedValues({ title, price, image, specs, description }) {
  featureDescSpacing(specs);
  currentComputerPrice = price;
  computerImgEl.src = defaultAssetUrl + image;
  computerDescEl.textContent = description;
  computerTitleEl.textContent = title;
  computerPriceEl.textContent = price + " NOK";
  visibleArticleSection();
}

/**
 * Splits the object value and creates an array. Creates child element for each child and appends to <ul>
 * @param {object} text
 */

function featureDescSpacing(text) {
  featureDescEl.innerHTML = "";

  const textSpacing = text.toString().split(",");

  for (let line of textSpacing) {
    const liChild = document.createElement("li");
    liChild.innerText = line;
    featureDescEl.appendChild(liChild);
  }
}

/**
 * A function that sends an alert if user has bought a computer and reduces user cash amount by the computer price
 * @returns nothing if it does not meet the conditions
 */
function buyComputer() {
  if (userCashAmount < currentComputerPrice) {
    alert("You cannot afford this computer! Take a loan or go to work!");
    return;
  }
  throwElement(balanceAmountEL);

  userCashAmount -= currentComputerPrice;
  updateBalances();
  alert("You are the owner of a new laptop!");
}

/**
 * A function to create option and populate the parent with children
 * @param {object} item
 */

function createSelectChild(item) {
  const option = document.createElement("option");
  option.text = item.title;
  option.value = item.title;
  selectComputerEl.appendChild(option);
}

function visibleArticleSection() {
  computerInfoEl.hidden = false;
}

function hiddenRepayLoanButton() {
  repayLoanButtonEL.hidden = true;
}

function visibleRepayLoanButton() {
  repayLoanButtonEL.hidden = false;
}

function visibleOutstandingLoan() {
  outstandingLoanEL.hidden = false;
  outstandingLoanAmountEL.hidden = false;
}

function hiddenOutstandingLoan() {
  outstandingLoanEL.hidden = true;
  outstandingLoanAmountEL.hidden = true;
}

function resetWorkingEarned() {
  workingEarned = 0;
}

function resetOutstandingAmount() {
  outstandingAmount = 0;
}

/**
 * a function to check if the user can loan, if yes then update correct global values and update the changes
 * if not then sends an alert and returns the user
 *
 * @returns returns the user back or alerts if the user does not meet the conditions
 */
function getLoan() {
  const LoanAmount = parseInt(prompt("How much do you want to loan?"));
  if (!LoanAmount) return;

  if (
    LoanAmount > userCashAmount * 2 ||
    Number.isNaN(LoanAmount) ||
    LoanAmount <= 0
  ) {
    alert("The bank cannot give a loan of this amount");
    return;
  } else if (parseInt(outstandingLoanAmountEL.textContent) > 0) {
    alert("Pay back the loan first before taking another loan!");
    return;
  }

  userCashAmount += LoanAmount;
  outstandingAmount = LoanAmount;

  visibleOutstandingLoan();
  visibleRepayLoanButton();

  updateBalances();
  throwElement(outstandingLoanAmountEL);
  spin(loanButtonEL);
}

/**
 *  a function to bank the money earned and pay 10% of the money for the outstanding loan or return full sum
 *
 * @returns the user back if he/she does not meet the conditions
 */

function bankEarnedMoney() {
  if (workingEarned <= 0) {
    alert("You need to work to bank cash!");
    return;
  }

  let deduction = 0.1 * workingEarned;

  workingEarned -= deduction;

  if (outstandingAmount - deduction >= 0) {
    outstandingAmount -= deduction;
    throwElement(outstandingLoanAmountEL);
  } else if (outstandingAmount - deduction < 0) {
    deduction -= outstandingAmount;
    userCashAmount += deduction;
    resetOutstandingAmount();
  }

  if (outstandingAmount === 0) {
    hiddenOutstandingLoan();
    hiddenRepayLoanButton();
  }

  userCashAmount += workingEarned;

  resetWorkingEarned();
  updateBalances();
  throwElement(balanceAmountEL);
  spin(bankButtonEL);
}

/**
 * updates the elements with correct balances
 */
function updateBalances() {
  balanceAmountEL.innerText = userCashAmount + " Kr.";
  workEarnedAmountEL.innerText = workingEarned + " Kr.";
  outstandingLoanAmountEL.innerText = outstandingAmount + " Kr.";
}

/**
 * a function to increase user cash by 100 and update the balances
 */
function earnMoney() {
  const cash = 100;

  workingEarned += cash;
  updateBalances();
  throwElement(workEarnedAmountEL);
  spin(workButtonEL);
}

/**
 * This function takes in an element as parameter and then does a throw effect and resets animation
 *
 */
function throwElement(element) {
  const styles = `#${element.id}{animation: throwElement 2s;}`;
  const styleSheet = document.createElement("style");
  styleSheet.innerText = styles;
  document.head.appendChild(styleSheet);
  element.style.animation = "none";
  element.offsetHeight; /* trigger reflow */
  element.style.animation = null;
}

/**
 * This function takes in an element as parameter and then does a spin effect and resets animation
 *
 */
function spin(element) {
  const styles = `#${element.id}{animation: spin 2s;}`;

  const styleSheet = document.createElement("style");
  styleSheet.innerText = styles;
  document.head.appendChild(styleSheet);
  element.style.animation = "none";
  element.offsetHeight; /* trigger reflow */
  element.style.animation = null;
}

/**
 * a function to repay all of the worked money to the outstanding loan and return all sum left to the user bank
 */
function repayTheDebt() {
  if (workingEarned > outstandingAmount) {
    workingEarned -= outstandingAmount;

    userCashAmount += workingEarned;

    resetOutstandingAmount();
    resetWorkingEarned();
    throwElement(balanceAmountEL);
  } else {
    outstandingAmount -= workingEarned;
    resetWorkingEarned();
    throwElement(outstandingLoanAmountEL);
  }

  if (outstandingAmount === 0) {
    hiddenOutstandingLoan();
    hiddenRepayLoanButton();
  }
  spin(repayLoanButtonEL);
  updateBalances();
}

//Event listeners
loanButtonEL.addEventListener("click", getLoan);
workButtonEL.addEventListener("click", earnMoney);
bankButtonEL.addEventListener("click", bankEarnedMoney);
repayLoanButtonEL.addEventListener("click", repayTheDebt);
selectComputerEl.addEventListener("change", AccessSelectedData);
buyNowButtonEl.addEventListener("click", buyComputer);
