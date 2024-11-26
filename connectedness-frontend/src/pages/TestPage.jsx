import React, { useEffect, useState } from "react";

export default function TestPage() {
  const [result, setResult] = useState(null);

  // Mock fetch (replace with real API)
  useEffect(() => {
    const fetchedResult = {
      groupId: 1,
      userId: 42,
      totalAnswered: 8,
      correctCount: 6,
      percentage: 75,
      answers: [
        {
          questionId: 101,
          questionText: "What is 2 + 2?",
          selectedAnswer: "4",
          correctAnswer: "4",
          isCorrect: true,
        },
        {
          questionId: 102,
          questionText: "Favorite color of sky?",
          selectedAnswer: "Green",
          correctAnswer: "Blue",
          isCorrect: false,
        },
      ],
    };
    setResult(fetchedResult);
  }, []);

  if (!result) {
    return (
      <div className="text-center py-20 text-gray-500">Loading result...</div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50 py-10 px-4">
      <div className="max-w-4xl mx-auto bg-white p-6 rounded-xl shadow-lg">
        <h2 className="text-2xl font-bold mb-6 text-center">
          Your Group Results
        </h2>
        <div className="grid grid-cols-2 gap-4 mb-6 text-center">
          <div className="bg-blue-100 p-4 rounded-lg">
            <p className="text-xl font-bold">{result.totalAnswered}</p>
            <p className="text-sm text-gray-700">Questions Answered</p>
          </div>
          <div className="bg-green-100 p-4 rounded-lg">
            <p className="text-xl font-bold">{result.correctCount}</p>
            <p className="text-sm text-gray-700">Correct Answers</p>
          </div>
        </div>
        <div className="mb-6 text-center">
          <p className="text-lg font-semibold">
            Score: <span className="text-green-600">{result.percentage}%</span>
          </p>
        </div>
        <hr className="mb-4" />
        <div>
          <h3 className="text-lg font-bold mb-3">Detailed Answers</h3>
          {result.answers.map((ans) => (
            <div
              key={ans.questionId}
              className={`mb-4 p-4 rounded-lg border ${
                ans.isCorrect
                  ? "border-green-300 bg-green-50"
                  : "border-red-300 bg-red-50"
              }`}
            >
              <p className="font-medium">{ans.questionText}</p>
              <p className="text-sm mt-1">
                Your Answer:{" "}
                <span className="font-semibold">{ans.selectedAnswer}</span>
              </p>
              <p className="text-sm">
                Correct Answer:{" "}
                <span className="font-semibold">{ans.correctAnswer}</span>
              </p>
              <p
                className={`mt-1 font-semibold ${
                  ans.isCorrect ? "text-green-600" : "text-red-600"
                }`}
              >
                {ans.isCorrect ? "Correct ✅" : "Incorrect ❌"}
              </p>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}
