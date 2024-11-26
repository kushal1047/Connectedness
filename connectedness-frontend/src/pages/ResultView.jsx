import { useState, useEffect } from "react";

export default function ResultView() {
  const [result, setResult] = useState(null);

  useEffect(() => {
    const fetchedResults = {
      groupId: 1,
      userId: 21,
      totalAnswered: 10,
      totalCorrect: 12,
      percentage: 75,
      answers: [
        {
          questionId: 101,
          questionText: "Whats my name?",
          selectedAnswer: "DJ Khaled",
          correctAnswer: "Kushal",
          isCorrect: false,
        },
        {
          questionId: 102,
          questionText: "What my favourite food?",
          selectedAnswer: "Apple",
          correctAnswer: "Apple",
          isCorrect: true,
        },
      ],
    };
    setResult(fetchedResults);
  }, []);
  if (!result) {
    return (
      <div className="text-center py-20 text-gray-500">Loading results...</div>
    );
  }
  return (
    <div className="min-h-screen bg-50 px-4 py-10">
      <div className="max-w-4xl mx-auto p-6 bg-white rounded-xl shadow-lg">
        <h2 className="text-2xl font-bold text-center mb-6">
          Your Group Result
        </h2>
        <div className="grid grid-cols-2 gap-4 mb-6 text-center">
          <div className="bg-blue-100 p-4 rounded-lg">
            <p className="text-xl font-bold">{result.totalAnswered}</p>
            <p className="text-sm text-gray-700">Total Questions Answered</p>
          </div>
          <div className="bg-green-100 rounded-lg p-4">
            <p className="text-xl font-bold">{result.totalCorrect}</p>
            <p className="text-sm text-gray-700">Correct Answers</p>
          </div>
        </div>
        <div className="text-center mb-6">
          <p className="text-lg font-semibold">
            Score: <span className="text-green-600">{result.percentage}%</span>
          </p>
        </div>
        <hr className="mb-4" />
        <div>
          <h3 className="text-xl font-bold mb-3">Detailed Answers</h3>
          {result.answers.map((answer) => (
            <div
              key={answer.questionId}
              className={`p-4 mb-4 border rounded-lg ${
                answer.isCorrect
                  ? "border-green-300 bg-green-50"
                  : "border-red-300 bg-red-50"
              }`}
            >
              <p className="font-medium mb-1">{answer.questionText}</p>
              <p className="text-sm">
                Selected Answer:{" "}
                <span className="font-semibold">{answer.selectedAnswer}</span>
              </p>
              <p className="text-sm">
                Correct Answer:{" "}
                <span className="font-semibold">{answer.correctAnswer}</span>
              </p>
              <p
                className={`mt-1 font-semibold ${
                  answer.isCorrect ? "text-green-600" : "text-red-600"
                }`}
              >
                {answer.isCorrect ? "Correct Answer ✅" : "Wrong Answer ❌"}
              </p>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}
