import { useState, useEffect } from "react";

export default function AnswerQuestions() {
  const [questions, setQuestions] = useState([]);
  const [answers, setAnswers] = useState({});

  useEffect(() => {
    const fetchedQuestions = [
      {
        questionId: 1,
        text: "What's the color of my car?",
        options: ["Red", "Blue", "Yellow", "Green"],
      },
      {
        questionId: 2,
        text: "What's my mother's maiden name?",
        options: ["Baral", "Ghimire", "Poudel", "Neupane"],
      },
    ];
    setQuestions(fetchedQuestions);
  }, []);

  const handleAddAnswer = (questionId, selectedAnswer) => {
    setAnswers((prev) => ({ ...prev, [questionId]: selectedAnswer }));
  };

  const submitAnswers = () => {
    const payload = Object.entries(answers).map(
      ([questionId, selectedAnswer]) => ({
        questionId: parseInt(questionId),
        selectedAnswer,
      })
    );
    console.log("answers submitted", payload);
  };
  return (
    <div className="min-h-screen bg-gray-50 px-4 py-10">
      <div className="max-w-4xl mx-auto bg-white shadow-lg p-5 rounded-2xl">
        <h2 className="text-2xl font-bold text-center mb-6">Group Questions</h2>
        {questions.map((question) => (
          <div
            key={question.questionId}
            className="border bg-gray-100 p-4 mb-6 rounded-lg"
          >
            <p className="text-lg font-medium mb-3">{question.text}</p>
            <div className="grid grid-cols-2 gap-3">
              {question.options.map((option) => (
                <label
                  key={option}
                  className={`cursor-pointer px-4 py-2 rounded-lg border text-center 
                            ${
                              answers[question.questionId] === option
                                ? "bg-blue-500 text-white"
                                : "bg-white hover:bg-blue-100"
                            }`}
                >
                  <input
                    type="radio"
                    name={`question ${question.questionId}`}
                    value={option}
                    className="hidden"
                    onChange={() => {
                      handleAddAnswer(question.questionId, option);
                    }}
                  />
                  {option}
                </label>
              ))}
            </div>
          </div>
        ))}
        <button
          type="button"
          onClick={submitAnswers}
          className="text-white mt-4 rounded-lg py-3 w-full font-medium text-lg bg-green-600 hover:bg-green-700"
        >
          Submit Answers
        </button>
      </div>
    </div>
  );
}
