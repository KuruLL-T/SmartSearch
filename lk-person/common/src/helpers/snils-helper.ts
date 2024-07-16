export function validateSnils(snils: string) {
  snils = snils.replace(/[-\s]/g, '');

  let sum = 0;
  for (let i = 0; i < 9; i++) {
    sum += parseInt(snils[i]) * (9 - i);
  }

  let checkDigit = 0;
  if (sum < 100) {
    checkDigit = sum;
  } else if (sum > 101) {
    checkDigit = sum % 101;
    if (checkDigit === 100) checkDigit = 0;
  }

  return checkDigit === parseInt(snils.slice(-2));
}
