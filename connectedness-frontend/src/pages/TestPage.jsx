import React, { useState, useEffect } from "react";

export default function TestPage() {
  const [questions, setQuestions] = useState([]);
  const [answers, setAnswers] = useState({});

  // Mock data fetch (replace with actual API later)
  useEffect(() => {
    const fetchedQuestions = [
      {
        questionId: 1,
        text: "What is your favorite color?",
        options: ["Red", "Blue", "Green", "Yellow"],
      },
      { questionId: 2, text: "Pick a number", options: ["1", "2", "3", "4"] },
    ];
    setQuestions(fetchedQuestions);
  }, []);

  const handleSelect = (questionId, selectedAnswer) => {
    setAnswers((prev) => ({ ...prev, [questionId]: selectedAnswer }));
  };

  const handleSubmit = () => {
    const payload = Object.entries(answers).map(
      ([questionId, selectedAnswer]) => ({
        questionId: parseInt(questionId),
        selectedAnswer,
      })
    );
    console.log("Submitted Answers:", payload);
    // TODO: Replace with POST to backend
  };

  return (
    <div className="min-h-screen bg-gray-50 py-10 px-4">
      <div className="max-w-4xl mx-auto bg-white p-6 rounded-xl shadow-lg">
        <h2 className="text-2xl font-bold mb-6 text-center">
          Answer Questions
        </h2>
        {questions.map((q) => (
          <div
            key={q.questionId}
            className="mb-6 border p-4 rounded-lg bg-gray-100"
          >
            <p className="text-lg font-medium mb-3">{q.text}</p>
            <div className="grid grid-cols-2 gap-3">
              {q.options.map((option) => (
                <label
                  key={option}
                  className={`cursor-pointer px-4 py-2 rounded-lg border text-center ${
                    answers[q.questionId] === option
                      ? "bg-blue-500 text-white"
                      : "bg-white hover:bg-blue-100"
                  }`}
                >
                  <input
                    type="radio"
                    name={`question-${q.questionId}`}
                    value={option}
                    className="hidden"
                    onChange={() => handleSelect(q.questionId, option)}
                  />
                  {option}
                </label>
              ))}
            </div>
          </div>
        ))}
        <button
          onClick={handleSubmit}
          className="w-full mt-4 bg-green-500 text-white py-3 rounded-lg hover:bg-green-600 text-lg"
        >
          Submit Answers
        </button>
      </div>
    </div>
  );
}
