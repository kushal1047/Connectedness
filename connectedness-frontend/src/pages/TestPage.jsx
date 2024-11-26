import React, { useState } from "react";

export default function TestPage() {
  const [questions, setQuestions] = useState([{ text: "", correctAnswer: "" }]);

  const addQuestion = () => {
    if (questions.length < 10) {
      setQuestions([...questions, { text: "", correctAnswer: "" }]);
    }
  };

  const handleInputChange = (index, field, value) => {
    const updated = [...questions];
    questions[index][field] = value;
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log("Submitted Questions:", questions);
    // TODO: Replace with API call
  };

  return (
    <div className="min-h-screen bg-gray-50 py-10 px-4">
      <div className="max-w-4xl mx-auto bg-white p-6 rounded-xl shadow-lg">
        <h2 className="text-2xl font-bold mb-4 text-center">
          Create Questions (Max 10)
        </h2>
        <form onSubmit={handleSubmit} className="space-y-6">
          {questions.map((q, idx) => (
            <div key={idx} className="border p-4 rounded-lg bg-gray-100">
              <label className="block text-sm font-semibold text-gray-700 mb-1">
                Question {idx + 1}
              </label>
              <input
                type="text"
                placeholder="Enter your question"
                value={q.text}
                onChange={(e) => handleInputChange(idx, "text", e.target.value)}
                className="w-full px-3 py-2 border rounded-lg focus:outline-none focus:ring focus:ring-blue-300 mb-2"
              />
              <input
                type="text"
                placeholder="Enter correct answer"
                value={q.correctAnswer}
                onChange={(e) =>
                  handleInputChange(idx, "correctAnswer", e.target.value)
                }
                className="w-full px-3 py-2 border rounded-lg focus:outline-none focus:ring focus:ring-green-300"
              />
            </div>
          ))}
          <div className="flex justify-between">
            <button
              type="button"
              onClick={addQuestion}
              disabled={questions.length >= 10}
              className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
            >
              Add Question
            </button>
            <button
              type="submit"
              className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600"
            >
              Submit
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
