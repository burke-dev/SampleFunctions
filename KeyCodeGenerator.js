function generateCode(segmentLength, segments) {
  return [...Array(segments)].map(segment => generateSegment(segmentLength)).join('-');
}

function generateSegment(segmentLength) {
  return [...Array(segmentLength)].map(entry => generateEntry()).join('');
}

function generateEntry() {
  return Math.floor(Math.random() * 16).toString(16);
}
